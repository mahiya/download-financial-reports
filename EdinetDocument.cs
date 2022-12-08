using Newtonsoft.Json;
using System.Collections.Generic;

namespace DownloadFinancialReports
{
    public class GetDocumentsApiResponse
    {
        [JsonProperty("results")]
        public List<EdinetDocument> Documents { get; set; }
    }

    public class EdinetDocument
    {
        [JsonProperty("docID")]
        public string DocID { get; set; }

        [JsonProperty("edinetCode")]
        public string EdinetCode { get; set; }

        [JsonProperty("secCode")]
        public string SecCode { get; set; }

        [JsonProperty("JCN")]
        public string JCN { get; set; }

        [JsonProperty("filerName")]
        public string FilerName { get; set; }

        [JsonProperty("fundCode")]
        public string FundCode { get; set; }

        [JsonProperty("ordinanceCode")]
        public string OrdinanceCode { get; set; }

        [JsonProperty("formCode")]
        public string FormCode { get; set; }

        [JsonProperty("docTypeCode")]
        public string DocTypeCode { get; set; }

        [JsonProperty("periodStart")]
        public string PeriodStart { get; set; }

        [JsonProperty("periodEnd")]
        public string PeriodEnd { get; set; }

        [JsonProperty("submitDateTime")]
        public string SubmitDateTime { get; set; }

        [JsonProperty("docDescription")]
        public string DocDescription { get; set; }

        [JsonProperty("issuerEdinetCode")]
        public string IssuerEdinetCode { get; set; }

        [JsonProperty("subjectEdinetCode")]
        public string SubjectEdinetCode { get; set; }

        [JsonProperty("subsidiaryEdinetCode")]
        public string SubsidiaryEdinetCode { get; set; }

        [JsonProperty("currentReportReason")]
        public object CurrentReportReason { get; set; }

        [JsonProperty("parentDocID")]
        public string ParentDocID { get; set; }

        [JsonProperty("opeDateTime")]
        public string OpeDateTime { get; set; }

        [JsonProperty("withdrawalStatus")]
        public string WithdrawalStatus { get; set; }

        [JsonProperty("docInfoEditStatus")]
        public string DocInfoEditStatus { get; set; }

        [JsonProperty("disclosureStatus")]
        public string DisclosureStatus { get; set; }

        [JsonProperty("xbrlFlag")]
        public string XbrlFlag { get; set; }

        [JsonProperty("pdfFlag")]
        public string PdfFlag { get; set; }

        [JsonProperty("attachDocFlag")]
        public string AttachDocFlag { get; set; }

        [JsonProperty("englishDocFlag")]
        public string EnglishDocFlag { get; set; }
    }
}
