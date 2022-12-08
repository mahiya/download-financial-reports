using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DownloadFinancialReports
{
    class Program
    {
        // ダウンロードしたPDFファイルを格納するフォルダ
        const string PdfFilesDirPath = "pdfs";

        public static async Task Main()
        {
            // ドキュメントのメタ情報をダウンロードする
            var documents = new List<EdinetDocument>();
            var from = new DateTime(2021, 1, 1); // 取得開始日
            var to = new DateTime(2021, 12, 31); // 取得終了日
            var date = from;
            while (date < to.AddDays(1))
            {
                Console.WriteLine($"Download document list of {date.ToString("yyyy/MM/dd")}");
                var url = $"https://disclosure.edinet-fsa.go.jp/api/v1/documents.json?date={date.ToString("yyyy-MM-dd")}&type=2";
                var resp = await url.GetAsync<GetDocumentsApiResponse>(delay: Random.Shared.Next(1000));
                documents.AddRange(resp.Documents);
                date = date.AddDays(1);
            }

            // 有価証券報告書でかつPDFファイルがアップロードされているドキュメントのみを対象とする
            documents = documents.Where(d => d.PdfFlag == "1").Where(d => d.DocTypeCode == "120").ToList();

            // 有価証券報告書のPDFファイルを格納するフォルダを準備する
            if (!Directory.Exists(PdfFilesDirPath))
                Directory.CreateDirectory(PdfFilesDirPath);

            // 有価証券報告書のPDFファイルをダウンロードする
            foreach (var (docId, index) in documents.Select((doc, index) => (doc.DocID, index)))
            {
                Console.WriteLine($"Downloading {docId} ({index + 1}/{documents.Count()})");
                var outputPath = Path.Combine(PdfFilesDirPath, $"{docId}.pdf");
                if (File.Exists(outputPath)) continue;

                var url = $"https://disclosure.edinet-fsa.go.jp/api/v1/documents/{docId}?type=2";
                var bytes = await url.GetBytesAsync(delay: Random.Shared.Next(1000));
                await File.WriteAllBytesAsync(outputPath, bytes);
            }
        }
    }
}
