using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using System.Data.SQLite;
using System.IO;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;

namespace _175_R
{
    public partial class Form1 : MetroForm
    {
        public string url = "https://api.binance.com/api/v3/ticker/price";
        public int isFirstProcess = 1;
        public int isRunning = 0;
        public Form1()
        {
            InitializeComponent();
            InitializeSQLite();
        }

        // 起動時SQLiteがなければ作る(Tableも)、初期値を入れる
        private void InitializeSQLite()
        {
            SQLiteConnection con = new SQLiteConnection("Data Source=mydb.sqlite;Version=3;");
            con.Open();
            try
            {
                using (var cmd = new SQLiteCommand(con))
                {
                    // テーブル作成(なければ)
                    cmd.CommandText = "DROP TABLE IF EXISTS datas;" +
                        "CREATE TABLE IF NOT EXISTS datas(" +
                        "name TEXT NOT NULL PRIMARY KEY," +
                        "price_now REAL NOT NULL," +
                        "price_last_5min REAL NOT NULL," +
                        "price_last_10min REAL NOT NULL," +
                        "price_last_15min REAL NOT NULL," +
                        "price_last_20min REAL NOT NULL," +
                        "price_up_count INTEGER NOT NULL," +
                        "is_price_up INTEGER NOT NULL)";
                    cmd.ExecuteNonQuery();
                    // 銘柄リストを読み込んでデータオブジェクトにする
                    List<DataClass> dataClasses = new List<DataClass>();
                    var fileName = @"Brand.txt";
                    var encoding = Encoding.GetEncoding("SHIFT_JIS");
                    using (var reader = new StreamReader(fileName, encoding))
                    {
                        while (!reader.EndOfStream)
                        {
                            var record = reader.ReadLine();
                            DataClass dataClass = new DataClass();
                            dataClass.name = record;
                            dataClass.price_now = 0;
                            dataClass.price_last_5min = 0;
                            dataClass.price_last_10min = 0;
                            dataClass.price_last_15min = 0;
                            dataClass.price_last_20min = 0;
                            dataClass.price_up_count = 0;
                            dataClass.is_price_up = 0;
                            dataClasses.Add(dataClass);
                        }
                    }
                    // SQLiteのデータを初期化
                    string sql = "INSERT INTO datas (name, price_now, price_last_5min, price_last_10min, price_last_15min, price_last_20min, price_up_count, is_price_up) VALUES ";
                    foreach (DataClass data in dataClasses)
                    {
                        sql += "("
                            + "'" + data.name + "'" + ","
                            + data.price_now + ","
                            + data.price_last_5min + ","
                            + data.price_last_10min + ","
                            + data.price_last_15min + ","
                            + data.price_last_20min + ","
                            + data.price_up_count + ","
                            + data.is_price_up
                        + ")";
                        sql += ",";
                    }
                    sql = sql.TrimEnd(',');
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                con.Close();
            }
        }

        // 開始ボタンクリック時
        private void StartProcess(object sender, EventArgs e)
        {
            if (isRunning == 0)
            {
                isRunning = 1;
                StartButton.Text = "Stop Jumping...";
                ProcessTimer.Start();
                MainProcess(new object(), new EventArgs());
            }
            else
            {
                ProcessTimer.Stop();
                InitializeProcess();
            }
        }

        // Binanceから初期データを取得(全データとってきてるのでparamで絞れるならそうしたいよね)
        private async Task<string> GetDatasFromBinance()
        {
            int NumberOfRetries = 3;
            int DelayOnRetry = 1000;
            using (var client = new HttpClient())
            {
                for (int i = 1; i <= NumberOfRetries; ++i)
                {
                    try
                    {
                        var response = await client.GetAsync(url);
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            return await response.Content.ReadAsStringAsync();
                        }
                        else
                        {
                            return "err";
                        }
                    }
                    catch (Exception err) when (i < NumberOfRetries)
                    {
                        await Task.Delay(DelayOnRetry);
                    }
                }
                return "err";
            }
        }

        // SQLiteから既存データを取得
        private List<DataClass> GetDataFromSQLite()
        {
            List<DataClass> dataClasses = new List<DataClass>();
            SQLiteConnection con = new SQLiteConnection("Data Source=mydb.sqlite;Version=3;");
            con.Open();
            try
            {
                using (var cmd = new SQLiteCommand(con))
                {
                    // テーブルのデータを取得
                    cmd.CommandText = "SELECT * FROM datas;";
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read() == true)
                        {
                            DataClass dataClass = new DataClass();
                            dataClass.name = reader.GetValue(0).ToString();
                            dataClass.price_now = Math.Round(double.Parse(reader.GetValue(1).ToString()), 8, MidpointRounding.AwayFromZero);
                            dataClass.price_last_5min = Math.Round(double.Parse(reader.GetValue(2).ToString()), 8, MidpointRounding.AwayFromZero);
                            dataClass.price_last_10min = Math.Round(double.Parse(reader.GetValue(3).ToString()), 8, MidpointRounding.AwayFromZero);
                            dataClass.price_last_15min = Math.Round(double.Parse(reader.GetValue(4).ToString()), 8, MidpointRounding.AwayFromZero);
                            dataClass.price_last_20min = Math.Round(double.Parse(reader.GetValue(5).ToString()), 8, MidpointRounding.AwayFromZero);
                            dataClass.price_up_count = int.Parse(reader.GetValue(6).ToString());
                            dataClass.is_price_up = int.Parse(reader.GetValue(7).ToString());
                            dataClasses.Add(dataClass);
                        }
                    }
                }
                return dataClasses;
            }
            finally
            {
                con.Close();
            }
        }

        // SQLiteのデータを更新
        private void SetDataInSQLite(List<DataClass> SQLiteDatas)
        {
            SQLiteConnection con = new SQLiteConnection("Data Source=mydb.sqlite;Version=3;");
            con.Open();
            try
            {
                using (var cmd = new SQLiteCommand(con))
                {
                    // テーブルのデータを上書き(UPSERT)
                    string sql = "INSERT INTO datas (name, price_now, price_last_5min, price_last_10min, price_last_15min, price_last_20min, price_up_count, is_price_up) VALUES ";
                    foreach (DataClass data in SQLiteDatas)
                    {
                        sql += "("
                            + "'" + data.name + "'" + ","
                            + data.price_now + ","
                            + data.price_last_5min + ","
                            + data.price_last_10min + ","
                            + data.price_last_15min + ","
                            + data.price_last_20min + ","
                            + data.price_up_count + ","
                            + data.is_price_up
                        + ")";
                        sql += ",";
                    }
                    sql = sql.TrimEnd(',');
                    sql += " ON CONFLICT(name) DO UPDATE SET "
                        + "price_now = excluded.price_now,"
                        + "price_last_5min = excluded.price_last_5min,"
                        + "price_last_10min = excluded.price_last_10min,"
                        + "price_last_15min = excluded.price_last_15min,"
                        + "price_last_20min = excluded.price_last_20min,"
                        + "price_up_count = excluded.price_up_count,"
                        + "is_price_up = excluded.is_price_up;";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                con.Close();
            }
        }

        // 各種フラグ・テーブル表示の初期化
        private void InitializeProcess()
        {
            // テーブルを初期化
            BrandItemsView.Rows.Clear();
            // SQLiteを初期化
            InitializeSQLite();
            // 動作中フラグを初期化
            isRunning = 0;
            // 初回動作フラグを初期化
            isFirstProcess = 1;
            // スタートボタンの表示を戻す
            StartButton.Text = "Let's Start Jumping!";
        }

        // データ取得・DB更新・テーブル表示更新のメイン処理
        private async void MainProcess(object sender, EventArgs e)
        {
            // Loading表示
            Loading.Visible = true;
            // 最終更新時間ラベルの表示をいったん空白に
            LastUpdate.Text = "";
            // StartButtonをdisabledにする
            StartButton.Enabled = false;
            // Binanceからデータを取得
            string datasFromBinance = await GetDatasFromBinance();
            List<ResponseData> tmpBinanceDatas = JsonConvert.DeserializeObject<List<ResponseData>>(datasFromBinance);
            // BTC建てのデータだけfilter
            List<ResponseData> BinanceDatas = new List<ResponseData>();
            foreach (ResponseData responseData in tmpBinanceDatas)
            {
                if (responseData.symbol.Contains("BTC"))
                {
                    ResponseData data = new ResponseData();
                    data.symbol = responseData.symbol;
                    data.price = responseData.price;
                    BinanceDatas.Add(data);
                }
            }
            // SQLiteから既存データを取得
            List<DataClass> SQLiteDatas = GetDataFromSQLite();
            // Binanceのデータと既存データを比較してテーブル用データとSQLite更新用データを作成
            List<TableDataClass> tableDataClasses = new List<TableDataClass>();
            foreach (DataClass SQLiteData in SQLiteDatas)
            {
                foreach (ResponseData BinanceData in BinanceDatas)
                {
                    if (SQLiteData.name == BinanceData.symbol)
                    {
                        // 30秒前、60秒前、90秒前の価格データをそれぞれ30秒後のものにスライド
                        SQLiteData.price_last_5min = SQLiteData.price_now;
                        SQLiteData.price_last_10min = SQLiteData.price_last_5min;
                        SQLiteData.price_last_15min = SQLiteData.price_last_10min;
                        SQLiteData.price_last_20min = SQLiteData.price_last_15min;
                        // 現在価格を更新
                        SQLiteData.price_now = Math.Round(double.Parse(BinanceData.price), 8, MidpointRounding.AwayFromZero);
                        // 価格上昇フラグを更新
                        if (isFirstProcess != 1)
                        {
                            if (SQLiteData.price_now >= SQLiteData.price_last_15min * 1.02)
                            {
                                SQLiteData.is_price_up = 1;
                                SQLiteData.price_up_count++;
                                // 価格上昇の銘柄のデータをテーブル用データに追加
                                TableDataClass tableDataClass = new TableDataClass();
                                double ratio = Math.Round((SQLiteData.price_now / SQLiteData.price_last_15min) * 100, 2, MidpointRounding.AwayFromZero);
                                tableDataClass.column_brand = SQLiteData.name;
                                tableDataClass.column_price = SQLiteData.price_now.ToString();
                                tableDataClass.column_continue = (SQLiteData.price_up_count * 5).ToString() + "分連続上昇中";
                                tableDataClass.column_up_ratio = ratio.ToString() + "%";
                                tableDataClasses.Add(tableDataClass);
                            }
                            else
                            {
                                SQLiteData.is_price_up = 0;
                                SQLiteData.price_up_count = 0;
                            }
                        }
                    }
                }
            }
            // テーブル用データを更新
            BrandItemsView.Rows.Clear();
            if (isFirstProcess != 1)
            {
                foreach (TableDataClass tableDataClass in tableDataClasses)
                {
                    BrandItemsView.Rows.Add(tableDataClass.column_brand, tableDataClass.column_price, tableDataClass.column_continue, tableDataClass.column_up_ratio);
                }
                // 最終更新時間ラベルの表示を更新
                LastUpdate.Text = "Last Update: " +  DateTime.Now.ToString("HH:mm:ss");
            }
            else
            {
                isFirstProcess = 0;
            }
            // SQLiteのデータを更新
            SetDataInSQLite(SQLiteDatas);
            // Loading非表示
            Loading.Visible = false;
            // StartButtonをenabledにする
            StartButton.Enabled = true;
        }

        // フォーム閉じられたとき
        private void CloseProcess(object sender, FormClosingEventArgs e)
        {
            ProcessTimer.Stop();
            InitializeProcess();
        }
    }
}
