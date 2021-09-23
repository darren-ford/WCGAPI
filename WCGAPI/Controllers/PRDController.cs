#define Enable_Test
#pragma warning disable CS1591

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using PRDRESTDataAccess;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Reflection;
using System.Diagnostics;
//using NLog;
using WinSCP;
using Newtonsoft.Json;

using System.Web;
using System.Net.Sockets;
using System.Web.Http.Controllers;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.IO;

namespace WCGAPI.Controllers
{
    /*
    public class customers
    {
        public string CustomerName { get; set; }
        public string AccountNumber { get; set; }
        public string ContractID { get; set; }
        public string MPNID { get; set; }
        public string VATID { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string CSPDiscount { get; set; }
        public string AzureDiscount { get; set; }
        public string CustomerType { get; set; }
    }
    public class LoginData
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class SearchData
    {
        public string Accountnumber { get; set; }
        public string Date { get; set; }
        public string ID { get; set; }
        public string ProductCode { get; set; }
    }*/
    //Input & Output Class Pairs
    /// <summary>
    /// 
    /// </summary>
    public class APIResults_Login_Input
    {
        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class APIResults_Login_Output
    {
        /// <summary>
        /// 
        /// </summary>
        public string Token { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class APIResults_Validate_Input
    {
        /// <summary>
        /// 
        /// </summary>
        public string LoginToken { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string MethodName { get; set; }

    }
    /// <summary>
    /// 
    /// </summary>
    public class APIResults_General_Output
    {
        public string data { get; set; }

    }
    /// <summary>
    /// 
    /// </summary>
    public class APIResults_Validate_Output
    {
        public string ConnectionString { get; set; }
        public string DatabaseID { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class APIResults_Validate_Output2
    {
        /// <summary>
        /// 
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DatabaseID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Code { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class APIResults_Error_Output
    {
        /// <summary>
        /// 
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }
    }
    public class APIResults_EmptyError_Output
    {
       
    }
    /// <summary>
    /// 
    /// </summary>
    public class APIResults_InvList_Input
    {
        /// <summary>
        /// Customer account number
        /// </summary>
        public string Customer { get; set; }
        /// <summary>
        /// Only return records created or modified since this date (YYYYMMDD format)
        /// </summary>
        public string Date { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class APIResults_InvList_Output
    {
        public string Invoice { get; set; }
        public DateTime PeriodStart { get; set; }
        public DateTime DueDate { get; set; }
        public float AmountExclVAT { get; set; }
        public float Cost { get; set; }
        public float Profit { get; set; }
        public float AmountInclVAT { get; set; }
        public float VATAmount { get; set; }
        public string Customer { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string InvoiceType { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class APIResults_TelProdList_Input
    {
        //<summary>
        //Invoice number
        //</summary>
        public string Invoice { get; set; }
        /// <summary>
        /// Only return records created or modified since this date (YYYYMMDD format)
        /// </summary>
        public string Date { get; set; }
        public string Product { get; set; }
        public string Telephone { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class APIResults_TelProdList_Outputv1
    {
        public string Customer { get; set; }
        public string Telephone { get; set; }
        public string Product { get; set; }
        public string Description { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime BillAnniversaryDate { get; set; }
        public string SubscriptionID { get; set; }
        public int Units { get; set; }
        public float Rate { get; set; }
        public float Cost { get; set; }
        public float BillAmount { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class APIResults_TelProdList_Output
    {
        public string Customer { get; set; }
        public string Telephone { get; set; }
        public string Product { get; set; }
        public string Description { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime BillAnniversaryDate { get; set; }
        public decimal Units { get; set; }
        public decimal Rate { get; set; }
        public decimal Cost { get; set; }
        public decimal BillAmount { get; set; }
        public string ServiceStatus { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class APIResults_CustProdList_Input
    {
        /// <summary>
        /// Customer account number
        /// </summary>
        public string Customer { get; set; }
        public string Invoice { get; set; }
        /// <summary>
        /// Product Code
        /// </summary>
        public string Product { get; set; }
        /// <summary>
        /// Only return records created or modified since this date (YYYYMMDD format)
        /// </summary>
        public string Date { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class APIResults_CustProdList_Output_v1
    {
        public string Customer { get; set; }
        public string Product { get; set; }
        public string Description { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime BillAnniversaryDate { get; set; }
        public string SubscriptionID { get; set; }
        public int Units { get; set; }
        public float Rate { get; set; }
        public float Cost { get; set; }
        public float BillAmount { get; set; }
        public string ServiceName { get; set; }
        public string ServiceStatus { get; set; }
        public float Usage { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class APIResults_CustProdList_Output
    {
        public string Customer { get; set; }
        public string Product { get; set; }
        public string Description { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime BillAnniversaryDate { get; set; }
        public decimal Units { get; set; }
        public decimal Rate { get; set; }
        public decimal Cost { get; set; }
        public decimal BillAmount { get; set; }
        public string ServiceStatus { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class APIResults_ProdList_Input
    {
        /// <summary>
        /// Product Code
        /// </summary>
        public string Product { get; set; }
        public string Invoice { get; set; }
        /// <summary>
        /// Only return records created or modified since this date (YYYYMMDD format)
        /// </summary>
        public string Date { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class APIResults_ProdList_Output
    {
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public string ProductGroup { get; set; }
        public string Period { get; set; }
    }
    /// <summary>
    /// Customer Details
    /// </summary>
    public class APIResults_CustDetails_Input
    {
        /// <summary>
        /// Unique per record in Marketplace, used as identifier of customer record 
        /// </summary>
        public string AccountNumber { get; set; }
        /// <summary>
        /// Account Number of Parent Record
        /// </summary>
        public string ParentAccountNumber { get; set; }
        /// <summary>
        /// Only return records created or modified since this date (YYYYMMDD format)
        /// </summary>
        public string Date { get; set; }
    }
    /// <summary>
    /// Customer Details
    /// </summary>
    public class APIResults_CustDetails_Output
    {
        /// <summary>
        /// Unique per record in Marketplace, used as identifier of customer record 
        /// </summary>
        public string AccountNumber { get; set; }
        /// <summary>
        /// Account Number of Parent Record
        /// </summary>
        public string Parent { get; set; }
        /// <summary>
        /// Non-unique company \ site name
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// Non-Unique value, multiple sites may be on same Contract ID
        /// </summary>
        public string ContractID { get; set; }
        /// <summary>
        /// Non-Unique value, multiple sites may be on same MPN ID
        /// </summary>
        public string MPNID { get; set; }
        public string VATID { get; set; }
        /// <summary>
        /// Reseller or Site
        /// </summary>
        public string CustomerType { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        /// <summary>
        /// Percentage discount applied after default mark-up
        /// </summary>
        public string CSPDiscount { get; set; }
        /// <summary>
        /// Percentage discount applied after default mark-up
        /// </summary>
        public string AzureDiscount { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class APIResults_CustDetails_Output_Extra
    {
        /// <summary>
        /// Unique per record in Marketplace, used as identifier of customer record 
        /// </summary>
        public string AccountNumber { get; set; }
        /// <summary>
        /// Account Number of Parent Record
        /// </summary>
        public string Parent { get; set; }
        /// <summary>
        /// Non-unique company \ site name
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// Non-Unique value, multiple sites may be on same Contract ID
        /// </summary>
        public string ContractID { get; set; }
        /// <summary>
        /// Non-Unique value, multiple sites may be on same MPN ID
        /// </summary>
        public string MPNID { get; set; }
        public string VATID { get; set; }
        /// <summary>
        /// Reseller or Site
        /// </summary>
        public string CustomerType { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        /// <summary>
        /// Percentage discount applied after default mark-up
        /// </summary>
        public float CSPDiscount { get; set; }
        /// <summary>
        /// Percentage discount applied after default mark-up
        /// </summary>
        public float AzureDiscount { get; set; }
        public string Email { get; set; }
        public string Domain { get; set; }
        public string Country { get; set; }
        public string Currency { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class APIResults_CustDetails_Output_Test
    {
        /// <summary>
        /// Unique per record in Marketplace, used as identifier of customer record 
        /// </summary>
        public string AccountNumber { get; set; }
        /// <summary>
        /// Account Number of Parent Record
        /// </summary>
        public string Parent { get; set; }
        /// <summary>
        /// Non-unique company \ site name
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// Non-Unique value, multiple sites may be on same Contract ID
        /// </summary>
        public string ContractID { get; set; }
        /// <summary>
        /// Non-Unique value, multiple sites may be on same MPN ID
        /// </summary>
        public string MPNID { get; set; }
        public string VATID { get; set; }
        /// <summary>
        /// Reseller or Site
        /// </summary>
        public string CustomerType { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        /// <summary>
        /// Percentage discount applied after default mark-up
        /// </summary>
        public float CSPDiscount { get; set; }
        /// <summary>
        /// Percentage discount applied after default mark-up
        /// </summary>
        public float AzureDiscount { get; set; }
        public string Email { get; set; }
        public string Domain { get; set; }
        public string Country { get; set; }
        public string Currency { get; set; }
        public DateTime EffectiveDate { get; set; }
    }

    /// <summary>
    /// Customer List
    /// </summary>
    public class APIResults_CustList_Input
    {
        /// <summary>
        /// Unique per record in Marketplace, used as identifier of customer record 
        /// </summary>
        public string AccountNumber { get; set; }
        /// <summary>
        /// Non-unique company \ site name
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// Only return records created or modified since this date (YYYYMMDD format)
        /// </summary>
        public string Date { get; set; }
    }
    ///<summary>
    /// Customer List
    ///</summary>
    public class APIResults_CustList_Output
    {
        ///<summary>
        /// Unique per record in Marketplace, used as identifier of customer record
        ///</summary>
        public string AccountNumber { get; set; }
        /// <summary>
        /// Non-unique value
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// Reseller or Site
        /// </summary>
        public string CustomerType { get; set; }
    }

    /// <summary>
    /// Usage Summary
    /// </summary>
    public class APIResults_UsageSummary_Input_v1
    {
        /// <summary>Test Description</summary>
        public string AccountNumber { get; set; }

        public string Invoice { get; set; }
        public string CLI { get; set; }

        public string DateFrom { get; set; }
        public string DateTo { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class APIResults_UsageSummary_Input
    {
        /// <summary>Test Description</summary>
        public string AccountNumber { get; set; }

        public string Invoice { get; set; }
        public string CLI { get; set; }
    }
    ///<summary>
    /// Usage Summary
    ///</summary>
    public class APIResults_UsageSummary_Outputv1
    {
        public string AccountNumber { get; set; }
        public string Invoice { get; set; }
        public string CallType { get; set; }
        public string CLI { get; set; }
        public decimal Calls { get; set; }
        public decimal Duration { get; set; }
        public decimal DataSize { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
    public class APIResults_UsageSummary_Output
    {
        public string AccountNumber { get; set; }
        public string ANumber { get; set; }
        public decimal Calls { get; set; }
        public decimal Seconds { get; set; }
        public decimal Amount { get; set; }
        public decimal Data { get; set; }
        public decimal SMS { get; set; }
        public decimal MMS { get; set; }
        public decimal Voice { get; set; }
        public decimal Incoming { get; set; }
        public decimal OnNet { get; set; }
        public decimal Roaming { get; set; }
        public decimal EuroTraveller { get; set; }
        public decimal WorldTraveller { get; set; }
        public string FormattedDuration { get; set; }
        public decimal DataGB { get; set; }

    }

    /// <summary>
    /// 
    /// </summary>
    public class APIResults_CustList_OutputTest
    {
        ///<summary>
        /// Unique per record in Marketplace, used as identifier of customer record
        ///</summary>
        public string AccountNumber { get; set; }
        /// <summary>
        /// Non-unique value
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// Reseller or Site
        /// </summary>
        public string CustomerType { get; set; }
        /// <summary>
        /// ID
        /// </summary>
        public int CustomerID { get; set; }
    }
    /*    public static string GetJSonContents(object rec)
        {
            string output = "";
            output = JsonConvert.SerializeObject(rec);
            return output;
        }*/

    /// <summary>
    /// 
    /// </summary>
    public class APIResults_InvLines_Input
    {
        public string InvoiceNumber { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class APIResults_InvLines_Output
    {
        public string InvoiceNumber { get; set; }
        public string Customer { get; set; }
        public string Product { get; set; }
        public int Units { get; set; }
        public DateTime LinePeriodStart { get; set; }
        public DateTime LinePeriodEnd { get; set; }
        public DateTime DueDate { get; set; }
        public float LineAmountExclVAT { get; set; }
        public float LineCost { get; set; }
        public float LineProfit { get; set; }
        public float LineAmountInclVAT { get; set; }
        public float VATAmount { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string InvoiceType { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class APIResults_CustTelList_Input
    {
        /// <summary>
        /// Customer account number
        /// </summary>
        public string Customer { get; set; }
        public string Telephone { get; set; }
        /// <summary>
        /// Only return records created or modified since this date (YYYYMMDD format)
        /// </summary>
        public string Date { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class APIResults_CustTelList_Output
    {
        public string Customer { get; set; }
        public string Telephone { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class APIResults_DailyCustProd_Input
    {
        /// <summary>
        /// Customer account number
        /// </summary>
        public string Customer { get; set; }
        /// <summary>
        /// Only return records created or modified since this date (YYYYMMDD format)
        /// </summary>
        public string Date { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class APIResults_DailyCustProd_Output
    {
        public string Customer { get; set; }
        public string Product { get; set; }
        public string Description { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime BillAnniversaryDate { get; set; }
        public string SubscriptionID { get; set; }
        public int Units { get; set; }
        public float Rate { get; set; }
        public float Cost { get; set; }
        public float MarkUpPercentage { get; set; }
        public float DiscountPercentage { get; set; }
        public float BillAmount { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class APIResults_DailyCustProdChanges_Input
    {
        /// <summary>
        /// Customer account number
        /// </summary>
        public string Customer { get; set; }
        /// <summary>
        /// Only return records created or modified since this date (YYYYMMDD format)
        /// </summary>
        public string Date { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class APIResults_DailyCustProdChanges_Output
    {
        public string Customer { get; set; }
        public string Product { get; set; }
        public string Description { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime BillAnniversaryDate { get; set; }
        public string SubscriptionID { get; set; }
        public int Units { get; set; }
        public float Rate { get; set; }
        public float Cost { get; set; }
        public float MarkUpPercentage { get; set; }
        public float DiscountPercentage { get; set; }
        public float BillAmount { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class APIResults_ProductStatus_Input
    {
        /// <summary>
        /// Customer account number
        /// </summary>
        public string Customer { get; set; }
        /// <summary>
        /// Product code
        /// </summary>
        public string Product { get; set; }
        /// <summary>
        /// Only return records created or modified since this date (YYYYMMDD format)
        /// </summary>
        public string Date { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class APIResults_ProductStatus_Output
    {
        public string Customer { get; set; }
        public string Product { get; set; }
        public string Description { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime BillAnniversaryDate { get; set; }
        public string SubscriptionID { get; set; }
        public int Units { get; set; }
        public float Rate { get; set; }
        public float Cost { get; set; }
        public float MarkUpPercentage { get; set; }
        public float DiscountPercentage { get; set; }
        public float BillAmount { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class products
    {
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public string ProductGroup { get; set; }
        public string Period { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class telephones
    {
        public decimal? Rowid { get; set; }
        public string Telephonenumber { get; set; }
        public decimal? Customer { get; set; }
        public decimal? Effectivedate { get; set; }
        public decimal? Expirydate { get; set; }
        public string Rateschemecode { get; set; }
        public string Description { get; set; }
        public string Linetype { get; set; }
        public string CostCentre1 { get; set; }
        public string CostCentre2 { get; set; }
        public string CostCentre3 { get; set; }
        public string CostCentre4 { get; set; }
        public string CostCentre5 { get; set; }
        public string CostCentre6 { get; set; }
        public string Createdby { get; set; }
        public decimal? Createdon { get; set; }
        public string Modifiedby { get; set; }
        public decimal? Modifiedon { get; set; }
        public string WholesaleRateSchemeCode { get; set; }
        public decimal? PROVIDER { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class APIResults_AddCustomer_Input
    {
        public string Customer { get; set; }
        public string Date { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class APIResults_UpdateCustomer_Input
    {
        public string Customer { get; set; }
        public string Date { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class APIResults_AddTelephone_Input
    {
        public string Customer { get; set; }
        public string Date { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class APIResults_UpdateTelephone_Input
    {
        public string Customer { get; set; }
        public string Date { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class APIResults_AddProduct_Input
    {
        public string Customer { get; set; }
        public string Date { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class APIResults_UpdateProduct_Input
    {
        public string Customer { get; set; }
        public string Date { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class APIResults_AddCustomer_Output
    {
        public string Customer { get; set; }
        public string Date { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class APIResults_AddTelephone_Output
    {
        public string Customer { get; set; }
        public string Date { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class APIResults_AddProduct_Output
    {
        public string Customer { get; set; }
        public string Date { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class APIResults_UpdateCustomer_Output
    {
        public string Customer { get; set; }
        public string Date { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class APIResults_UpdateTelephone_Output
    {
        public string Customer { get; set; }
        public string Date { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class APIResults_UpdateProduct_Output
    {
        public string Customer { get; set; }
        public string Date { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ftpFiles
    {
        public string Name { get; set; }
        //        public string FullName { get; set; }
        public string FileType { get; set; }
        public decimal? Length { get; set; }
        //        public decimal? Length32 { get; set; }
        //        public string LastWriteTime { get; set; }
        //        public string FilePermissions { get; set; }
        //        public string Owner { get; set; }
        //        public string Group { get; set; }
        //        public string IsDirectory { get; set; }
        //        public string IsThisDirectory { get; set; }
        //        public string IsParentDirectory { get; set; }
    }



    /// <summary>
    /// 
    /// </summary>
    public class GetCustomers
    {
        public string AccountNumber { get; set; }
        public string CustomerName { get; set; }

        public int? InvoiceDay { get; set; }

    }

    public class CustomerDetails
    {
        public string Parent { get; set; }
        public string AccountNumber { get; set; }
        public string CustomerName { get; set; }
        public string VATID { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostCode { get; set; }
        public string RateScheme { get; set; }
        public string Currency { get; set; }
        public string CustomerManager { get; set; }
        public string Email { get; set; }
        public string InvoiceGroup { get; set; }
        public int? InvoiceDay { get; set; }
        public string ExpiryDate { get; set; }

    }

    public class CreateCustomer : CustomerDetails
    {
       
    }
    public class UpdateCustomer : CustomerDetails
    {

    }

    public class TerminateCustomer
    {
        public string AccountNumber { get; set; }
        public string ExpiryDate { get; set; }
    }

    public class GetTelephones
    {
        public string AccountNumber { get; set; }
        public string TelephoneNumber { get; set; }
        public string RateScheme { get; set; }
        public string Date { get; set; }
    }

    public class Telephone
    {
        public string AccountNumber { get; set; }
        public string TelephoneNumber { get; set; }
        public string RateScheme { get; set; }
        public string EffectiveDate { get; set; }
        public string ExpiryDate { get; set; }

    }

    public class CreateTelephone : Telephone
    {

    }
    public class UpdateTelephone : Telephone
    {

    }
    public class TerminateTelephone
    {
        public string TelephoneNumber { get; set; }
        public string ExpiryDate { get; set; }
    }
    public class GetRateSchemes
    {
        public string RateScheme { get; set; }
        public string Description { get; set; }
    }

    public class Rate_Scheme
    {
        public string RateScheme { get; set; }
        public string Description { get; set; }

    }

    public class CreateRateScheme : Rate_Scheme
    {

    }
    public class UpdateRateScheme : Rate_Scheme
    {

    }

    public class GetRates
    {
        public string RateScheme { get; set; }
        public string DestinationArea { get; set; }
        public string DestinationCountry { get; set; }
        public string Date { get; set; }
        public int? RateID { get; set; }
    }
    public class GetRate
    {
        public int? RateID { get; set; }
    }

    public class Rate
    {
        public int? RateID { get; set; }
        public string RateScheme { get; set; }
        public string DestinationArea { get; set; }
        public string DestinationCountry { get; set; }
        public string CallType { get; set; }
        public string ServiceClass { get; set; }
        public string TimeBand { get; set; }
        public Decimal? PricePerMinute { get; set; }
        public Decimal? PricePerCall { get; set; }
        public string Minimum { get; set; }
        public string Markup { get; set; }
        public string EffectiveDate { get; set; }
        public string ExpiryDate { get; set; }

    }

    public class CreateRate : Rate
    {

    }
    public class UpdateRate : Rate
    {

    }
    public class GetRateOverrides
    {
        public string AccountNumber { get; set; }
        public string DestinationArea { get; set; }
        public string Prefix { get; set; }
    }
    public class RateOverride
    {
        public string AccountNumber { get; set; }
        public string DestinationArea { get; set; }
        public string Prefix { get; set; }
        public string Minimum { get; set; }
        public Decimal? PeakPPM { get; set; }
        public Decimal? PeakPPC { get; set; }
        public Decimal? OffPeakPPM { get; set; }
        public Decimal? OffPeakPPC { get; set; }
        public Decimal? WeekendPPM { get; set; }
        public Decimal? WeekendPPC { get; set; }
    }

    public class CreateRateOverride : RateOverride
    {

    }
    public class UpdateRateOverride : RateOverride
    {

    }
    public class DeleteRateOverride : GetRateOverrides
    {

    }
    public class CreateRateOverrides
    {
        public string AccountNumber { get; set; }
        public string AreaList { get; set; }
        public string Minimum { get; set; }
        public Decimal? PeakPPM { get; set; }
        public Decimal? PeakPPC { get; set; }
        public Decimal? OffPeakPPM { get; set; }
        public Decimal? OffPeakPPC { get; set; }
        public Decimal? WeekendPPM { get; set; }
        public Decimal? WeekendPPC { get; set; }
    }
    public class GetProducts
    {
        public string ProductCode { get; set; }
        public string ProductGroup { get; set; }
        public string Description { get; set; }
        public string Period { get; set; }
        public string ProductType { get; set; }
        public string Date { get; set; }
    }
    public class Product
    {
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public string ProductGroup { get; set; }
        public string Period { get; set; }
        public string ProductType { get; set; }
        public Decimal? DefaultBuyPrice { get; set; }
        public Decimal? DefaultSellPrice { get; set; }
        public int? MonthsInAdvance { get; set; }

    }
    public class CreateProduct : Product
    {

    }
    public class UpdateProduct : Product
    {

    }

    public class GetProductBundleDetails
    {
        public string Product { get; set; }
        public string Area { get; set; }
        public string CallType { get; set; }
        public string Prefix { get; set; }
        public string PBDID { get; set; }
    }
    public class ProductBundleDetail
    {
        public string PBDID { get; set; }
        public string ProductCode { get; set; }
        public string Area { get; set; }
        public string CallType { get; set; }
        public string Prefix { get; set; }
        public string TimeBand { get; set; }
        public string InternalCalls { get; set; }
    }
    public class CreateProductBundleDetail : ProductBundleDetail
    {

    }
    public class UpdateProductBundleDetail : ProductBundleDetail
    {

    }

    public class GetProductBundleTiers
    {
        public string Product { get; set; }
        public decimal? Floor { get; set; }
        public string PBTID { get; set; }
    }
    public class ProductBundleTier
    {
        public string PBTID { get; set; }
        public string ProductCode { get; set; }
        public decimal? Floor { get; set; }
        public decimal? Ceiling { get; set; }
        public decimal? PPI { get; set; }
        public decimal? PPV { get; set; }
        public string UnitType { get; set; }
        public decimal? TierCost { get; set; }
    }
    public class CreateProductBundleTier : ProductBundleTier
    {

    }
    public class UpdateProductBundleTier : ProductBundleTier
    {

    }

    public class GetDestinations
    {
        public string Area { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public int? Mobile { get; set; }
    }
    public class Destination
    {
        public string Area { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public int? Mobile { get; set; }

    }
    public class CreateDestination : Destination
    {

    }
    public class UpdateDestination : Destination
    {

    }

    public class GetDestinationPrefixes
    {
        public string Area { get; set; }
        public string Country { get; set; }
        public string Prefix { get; set; }
        public string Date { get; set; }
    }
    public class DestinationPrefix
    {
        public string Area { get; set; }
        public string Country { get; set; }
        public string Prefix { get; set; }
        public string EffectiveDate { get; set; }
        public string ExpiryDate { get; set; }

    }
    public class CreateDestinationPrefix : DestinationPrefix
    {

    }
    public class UpdateDestinationPrefix : DestinationPrefix
    {

    }

    public class GetCustomerProducts
    {
        public string AccountNumber { get; set; }
        public string Telephone { get; set; }
        public string Product { get; set; }
        public string Date { get; set; }
        public string CPID { get; set; }
    }
    public class CustomerProduct
    {
        public string CPID { get; set; }
        public string AccountNumber { get; set; }
        public string Telephone { get; set; }
        public string ProductCode { get; set; }
        public int? Units { get; set; }
        public string EffectiveDate { get; set; }
        public string NextBillStartDate { get; set; }
        public string ExpiryDate { get; set; }
        public string DescriptionOverride { get; set; }
        public string Currency { get; set; }
        public int? ApplyRateOverride { get; set; }
        public decimal? RateOverride { get; set; }
        public decimal? CostOverride { get; set; }
        public int? ShowOnInvoice { get; set; }
    }
    public class CreateCustomerProduct : CustomerProduct
    {

    }
    public class UpdateCustomerProduct : CustomerProduct
    {

    }
    public class GetInvoices
    {
        public string AccountNumber { get; set; }
        public string Date { get; set; }
    }
    public class Invoice
    {
        public string InvoiceNumber { get; set; }
        public string PeriodStart   { get; set; }
        public string DueDate { get; set; }
        public string AmountExclVAT { get; set; }
        public string Cost { get; set; }
        public string Profit { get; set; }
        public string AmountInclVAT { get; set; }
        public string VATAmount { get; set; }
        public string Customer { get; set; }
        public string InvoiceDate { get; set; }
        public string InvoiceType { get; set; }

    }
    public class GetInvoiceLines
    {
        public string InvoiceNumber { get; set; }
    }
    public class InvoiceLine
    {
        public string InvoiceNumber { get; set; }
        public string ProductCode{ get; set; }
        public string Units{ get; set; }
        public string LinePeriodStart{ get; set; }
        public string LinePeriodEnd{ get; set; }
        public string DueDate{ get; set; }
        public string LineAmountExclVAT{ get; set; }
        public string LineCost{ get; set; }
        public string LineProfit{ get; set; }
        public string LineAmountInclVAT{ get; set; }
        public string LineVATAmount{ get; set; }
        public string Customer{ get; set; }
        public string InvoiceDate { get; set; }
        public string InvoiceType { get; set; }
        public string CPID { get; set; }

    }
    public class GetSummarisedCalls
    {
        public string InvoiceNumber { get; set; }
    }
    public class SummarisedCalls
    {
        public string InvoiceNumber { get; set; }
        public string AccountNumber { get; set; }
        public string ProductCode { get; set; }
        public string Calls { get; set; }
        public string PreDiscountAmount { get; set; }
        public string DiscountAmount { get; set; }
        public string Amount { get; set; }
        public string Duration { get; set; }
        public string DiscountedDuration { get; set; }
    }
    public class GetUnbilledCDRs
    {
        public string AccountNumber { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
    public class CDR
    {

        public string AccountNumber { get; set; }
        public string Invoice { get; set; }
        public string StartDate { get; set; }
        public string StartTime { get; set; }
        public string ANumber { get; set; }
        public string BNumber { get; set; }
        public string CNumber { get; set; }
        public string ServiceClass { get; set; }
        public string Timeband { get; set; }
        public string DestinationArea { get; set; }
        public string CallType { get; set; }
        public string Seconds { get; set; }
        public string Amount { get; set; }
        public string PreDiscountAmount { get; set; }
        public string ProductCode { get; set; }
        public string Currency { get; set; }
        public string PreratedCost { get; set; }
        public string Cost { get; set; }
        public string RateID { get; set; }
        public string RateScheme { get; set; }

    }

    public class GetBilledCDRs
    {
        public string AccountNumber { get; set; }
        public string InvoiceNumber { get; set; }
    }
    public class GetFilteredCDRs
    {
        public string AccountNumber { get; set; }
        public string InvoiceNumber { get; set; }

        public string ANumber { get; set; }

        public string BNumber { get; set; }

        public string CNumber { get; set; }

        public string ServiceClass { get; set; }

        public string TimeBand { get; set; }
        public string DestinationArea { get; set; }
        public string DestinationCountry { get; set; }
        public string CallType { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string FileName { get; set; }
        public decimal? MinValue { get; set; }
        public decimal? MaxValue { get; set; }
    }
    public class GetInvalidCDRs
    {
        public string StartDate { get; set; }

        public string EndDate { get; set; }
    }
    public class InvalidCDR
    {
        public string AccountNumber { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorDescription { get; set; }
        public string StartDate { get; set; }
        public string StartTime { get; set; }
        public string ANumber { get; set; }
        public string BNumber { get; set; }
        public string CNumber { get; set; }
        public string ServiceClass { get; set; }
        public string Timeband { get; set; }
        public string DestinationArea { get; set; }
        public string CallType { get; set; }
        public string Seconds { get; set; }
        public string Amount { get; set; }
        public string Currency { get; set; }
        public string PreratedCost { get; set; }
        public string Cost { get; set; }
        public string RateID { get; set; }
        public string RateScheme { get; set; }
    }
    public class GetInvoiceStatus
    {
        public string Date { get; set; }
    }
    public class InvoiceStatus
    {
        public int InvoiceRunID { get; set; }
        public string PeriodEnd { get; set; }
        public string PeriodStart { get; set; }
        public string InvoiceGroup { get; set; }
        public string Amount { get; set; }
        public string TaxAmount { get; set; }
        public string TotalAmount { get; set; }
        public string Cost { get; set; }
        public string Profit { get; set; }
        public int InvoiceCount { get; set; }
        public int ProcessingInvoices { get; set; }
        public int CompleteInvoices { get; set; }
        public int ErroredInvoices { get; set; }
        public int ToBeEmailedInvoices { get; set; }
        public int EmailedInvoices { get; set; }
        public int UnbilledInvoices  { get; set; }
    }
    public class GetAPILog
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
    public class APILog
    {
        public string APIPath { get; set; }
        public string EventDate { get; set; }
        public string EventTime { get; set; }
        public string APIParameters { get; set; }
        public string UserName { get; set; }
        public string IPAddress { get; set; }
    }
    public class GetInvoiceGroups
    {
        public string InvoiceGroupID { get; set; }
    }
    public class InvoiceGroup
    {

        public string InvoiceGroupID { get; set; }
        public string Description { get; set; }
        public string Period { get; set; }
        public string PreviousBillingDate { get; set; }
        public string InvoiceFormat { get; set; }
    }
    public class RerateCDRs
    {
        public string AccountNumber { get; set; }
    }
    public class RecycleErrors
    {
    }
    public class RunInvoiceGroup
    {
        public string InvoiceGroupID { get; set; }
    }
    public class RunSingleInvoice
    {
        public string AccountNumber { get; set; }
        public string InvoiceType { get; set; }
        public string InvoiceEndDate { get; set; }
    }
    public class UnbillInvoice
    {
        public string InvoiceNumber { get; set; }
    }
    public class RunReport
    {
        public int? ReportID { get; set; }
        public string Param1 { get; set; }
        public string Param2 { get; set; }
        public string Param3 { get; set; }
        public string Param4 { get; set; }
        public string Param5 { get; set; }
        public string Param6 { get; set; }
    }
    public class Report
    {
        public int? ReportID { get; set; }
        public string ReportName { get; set; }
        public string ReportData { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    static public class DbUtil

    {
        static public string message;
        static public SqlConnection GetConnection()
        {
            string conStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(conStr);
            DbUtil.message = "";
            con.InfoMessage += delegate (object sender, SqlInfoMessageEventArgs e)
            {
                if (DbUtil.message.Length > 0) DbUtil.message += "\n";
                DbUtil.message += e.Message;
            };
            return con;
        }
        static public SqlConnection GetConnection_Target(string conStr)
        {
            if (conStr.Equals(""))
                conStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(conStr);
            DbUtil.message = "";
            con.InfoMessage += delegate (object sender, SqlInfoMessageEventArgs e)
            {
                if (DbUtil.message.Length > 0) DbUtil.message += "\n";
                DbUtil.message += e.Message;
            };
            return con;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class DbExtensions
    {
        public static List<T> ToListCollection<T>(this DataTable dt)
        {
            List<T> lst = new System.Collections.Generic.List<T>();
            Type tClass = typeof(T);
            PropertyInfo[] pClass = tClass.GetProperties();
            List<DataColumn> dc = dt.Columns.Cast<DataColumn>().ToList();
            T cn;
            foreach (DataRow item in dt.Rows)
            {
                cn = (T)Activator.CreateInstance(tClass);
                foreach (PropertyInfo pc in pClass)
                {
                    // Can comment try catch block. 
                    try
                    {
                        DataColumn d = dc.Find(c => c.ColumnName == pc.Name);
                        if (d != null && item[pc.Name] != DBNull.Value)
                            pc.SetValue(cn, item[pc.Name], null);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                lst.Add(cn);
            }
            return lst;
        }
    }
    /*
   // [Authorize]
    public class customerController : ApiController
    {
        
        //private static Logger logger = LogManager.GetCurrentClassLogger();
        public IEnumerable<customers> Get()
        {
            using (SqlConnection con = DbUtil.GetConnection())
            {
                SqlCommand com = new SqlCommand("AuthAPI_Customers_get", con);
                com.CommandType = CommandType.StoredProcedure;
                SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.Int);
                RetVal.Direction = ParameterDirection.ReturnValue;
                SqlDataAdapter da = new SqlDataAdapter(com);
                con.Open();
                DataSet ds = new DataSet();
                da.Fill(ds);
                da.Dispose();
                //      logger.Info("customers_get:, return={0}", RetVal.Value);
                DataTable dt = ds.Tables[0];
                List<customers> ret = dt.ToListCollection<customers>();
                return ret.AsEnumerable<customers>();
            }
        }
        
        
        public customers Get(int? ID)
        {
            using (SqlConnection con = DbUtil.GetConnection())
            {
                SqlCommand com = new SqlCommand("AuthAPI_Customers_get", con);
                com.CommandType = CommandType.StoredProcedure;
                SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.Int);
                RetVal.Direction = ParameterDirection.ReturnValue;
                com.Parameters.Add("ID", SqlDbType.Int).Value = ID;
                SqlDataAdapter da = new SqlDataAdapter(com);
                con.Open();
                DataSet ds = new DataSet();
                da.Fill(ds);
                da.Dispose();
                //      logger.Info("customers_get:@ID={0}, return={1}", ID, RetVal.Value);
                if (ds.Tables.Count == 0)
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                    return dt.ToListCollection<customers>()[0];
                else
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }

        }
        
    }
*/
    /*
        [Authorize]
        public class productController : ApiController
        {

            [HttpPost]
            [Route("api/BodyTypes/Raw")]
            public string PostRawBuffer(string raw)
            {
                return raw;
            }
            //   private static Logger logger = LogManager.GetCurrentClassLogger();

            // GET api/values
            [HttpPost]
            [Route("api/BodyTypes/JsonStringBody")]
            public string JsonPlainBody([FromBody] string content)
            {
                return content;
            }
            [HttpPost]
            [Route("api/BodyTypes/PJS1")]
            public string PostJsonString([FromBody] string text)
            {
                return text;
            }
            [HttpPost]
            [Route("api/BodyTypes/PJS")]
            public string PostMultipleSimpleValues(string name, int value, DateTime entered, string action = null)
            {
                return string.Format("Name: {0}, Value: {1}, Date: {2}, Action: {3}", name, value, entered, action);
            }


            //   private static Logger logger = LogManager.GetCurrentClassLogger();
            public IEnumerable<products> Get()
            {
                using (SqlConnection con = DbUtil.GetConnection())
                {
                    SqlCommand com = new SqlCommand("AuthAPI_Products_get", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.Int);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //     logger.Info("products_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    List<products> ret = dt.ToListCollection<products>();
                    return ret.AsEnumerable<products>();
                }

            }

            public products Get(int? ID)
            {
                using (SqlConnection con = DbUtil.GetConnection())
                {
                    //int ou=Download();
                    //List<RemoteFileInfo> lrfi = ListDirectory("");
                    SqlCommand com = new SqlCommand("AuthAPI_Products_get", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.Int);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("ID", SqlDbType.Int).Value = ID;
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //     logger.Info("products_get:@ID={0}, return={1}", ID, RetVal.Value);
                    if (ds.Tables.Count == 0)
                        throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                        return dt.ToListCollection<products>()[0];
                    else
                        throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
                }

            }

            public int Download()
            {
                try
                {
                    // Setup session options
                    SessionOptions sessionOptions = new SessionOptions
                    {
                        Protocol = Protocol.Ftp,
                        HostName = "portal.p-rd.com",
                        UserName = "AlsoAzure",
                        Password = "AL8gDy7c#2sJz1",

                    };

                    using (Session session = new Session())
                    {
                        // Connect
                        session.Open(sessionOptions);

                                         // Upload files
                                           TransferOptions transferOptions = new TransferOptions();
                                           transferOptions.TransferMode = TransferMode.Binary;

                                           TransferOperationResult transferResult;
                                           transferResult =
                                               session.PutFiles(@"dc\toupload\*", "/home/user/", false, transferOptions);

                                           // Throw on any error
                                           transferResult.Check();

                                           // Print results
                                           foreach (TransferEventArgs transfer in transferResult.Transfers)
                                           {
                                               Console.WriteLine("Upload of {0} succeeded", transfer.FileName);
                                           }

                        // Download files
                        TransferOptions transferOptions = new TransferOptions();
                        transferOptions.TransferMode = TransferMode.Binary;

                        TransferOperationResult transferResult;
                        transferResult =
                            session.GetFiles("/processed/test.txt", @"c:\download\", false, transferOptions);

                        // Throw on any error
                        transferResult.Check();

                        // Print results
                        foreach (TransferEventArgs transfer in transferResult.Transfers)
                        {
                            Console.WriteLine("Download of {0} succeeded", transfer.FileName);
                        }
                    }

                    return 0;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: {0}", e);
                    return 1;
                }

            }

        }
        */
    /*
    public class ftpFileController : ApiController
    {
        public IEnumerable<ftpFiles> Get()
        {
            List<ftpFiles> ret = new List<ftpFiles>();
            try
            {
                // Setup session options
                SessionOptions sessionOptions = new SessionOptions
                {
                    Protocol = Protocol.Ftp,
                    HostName = "portal.p-rd.com",
                    UserName = "AlsoAzure",
                    Password = "AL8gDy7c#2sJz1",

                };

                using (Session session = new Session())
                {
                    // Connect
                    session.Open(sessionOptions);

                    RemoteDirectoryInfo directory =
                        session.ListDirectory("/Processed/");

                    ftpFiles instance = new ftpFiles();
                    foreach (RemoteFileInfo fileInfo in directory.Files)
                    {
                        instance.Name = fileInfo.Name;
//                        instance.FullName = fileInfo.FullName;
                        instance.FileType = fileInfo.FileType.ToString();
                        instance.Length = fileInfo.Length;
//                        instance.Length32 = fileInfo.Length32;
//                        instance.LastWriteTime = fileInfo.LastWriteTime.ToString();
                        //instance.FilePermissions = fileInfo.FilePermissions.ToString();
                        //instance.Owner = fileInfo.Owner;
                        //instance.Group = fileInfo.Group;
                        //instance.IsDirectory = fileInfo.IsDirectory.ToString();
                        //instance.IsThisDirectory = fileInfo.IsThisDirectory.ToString();
                        //instance.IsParentDirectory = fileInfo.IsParentDirectory.ToString();
                        ret.Add(instance);
                        Console.WriteLine(
                            "{0} with size {1}, permissions {2} and last modification at {3}",
                            fileInfo.Name, fileInfo.Length, fileInfo.FilePermissions,
                            fileInfo.LastWriteTime);
                    }
                    
                    //return ret.AsEnumerable<RemoteFileInfo>();
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e);
                //return 1;
            }
            return ret.AsEnumerable<ftpFiles>();

        }

    }
*/

    /*
public class telephoneController : ApiController
{

    public IEnumerable<telephones> Get()
    {
        using (SqlConnection con = DbUtil.GetConnection())
        {
            SqlCommand com = new SqlCommand("AuthAPI_Telephones_get", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.Int);
            RetVal.Direction = ParameterDirection.ReturnValue;
            SqlDataAdapter da = new SqlDataAdapter(com);
            con.Open();
            DataSet ds = new DataSet();
            da.Fill(ds);
            da.Dispose();
            //      logger.Info("customers_get:, return={0}", RetVal.Value);
            DataTable dt = ds.Tables[0];
            List<telephones> ret = dt.ToListCollection<telephones>();
            return ret.AsEnumerable<telephones>();
        }

    }

    public IEnumerable<telephones> Get(string ID)
    {
        using (SqlConnection con = DbUtil.GetConnection())
        {
            SqlCommand com = new SqlCommand("AuthAPI_Telephones_get", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.Int);
            RetVal.Direction = ParameterDirection.ReturnValue;
            com.Parameters.Add("TelephoneNumber", SqlDbType.VarChar, 20).Value = ID;
            SqlDataAdapter da = new SqlDataAdapter(com);
            con.Open();
            DataSet ds = new DataSet();
            da.Fill(ds);
            da.Dispose();
            DataTable dt = ds.Tables[0];
            List<telephones> ret = dt.ToListCollection<telephones>();
            return ret.AsEnumerable<telephones>();
        }

    }

}
*/

    /*
        public class sessionController : ApiController
        {

            [HttpPost]
            [Route("api/Login")]
            public string Authenticate(LoginData login)
            {
                string username = login.Username;
                string password = login.Password;
                return string.Format("NewToken: (" + username + ") (" + password + ")");
            }
        }
        */

    /*
class Validator2
{
    static string page = "";
    public static async void validate(string call)
    {
        page = call;
        // Run the task.
        System.Threading.Tasks.Task.Run(new Action(DownloadPageAsync));
        Console.ReadLine();
    }

    static HttpClient _client = new HttpClient();

    static async void DownloadPageAsync()
    {
        // Use static HttpClient to avoid exhausting system resources for network connections.
        var result = await _client.GetAsync(page);
        // Write status code.
        Console.WriteLine("STATUS CODE: " + result.StatusCode);
    }
}


class Validator
{
    static string page = "";
    public static async void validate(string call)
    {
        page = call;
        // Run the task.
        // System.Threading.Tasks.Task.Run(new Action(DownloadPageAsync));
        //Console.ReadLine();
        RunAsync();
    }

    static HttpClient client = new HttpClient();

    static async Task<Uri> CreateProductAsync(string product)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync(
            "Token", product);
        response.EnsureSuccessStatusCode();

        // return URI of the created resource.
        return response.Headers.Location;
    }

    static async Task RunAsync()
    {
        // Update port # in the following line.
        client.BaseAddress = new Uri("http://localhost:49333/");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        try
        {

            var url = await CreateProductAsync(page);
            Console.WriteLine($"Created at {url}");

            // Get the product
            //product = await GetProductAsync(url.PathAndQuery);
            //ShowProduct(product);

            // Update the product
            //Console.WriteLine("Updating price...");
            //product.Price = 80;
            //await UpdateProductAsync(product);

            // Get the updated product
            //product = await GetProductAsync(url.PathAndQuery);
            //ShowProduct(product);

            // Delete the product
            //var statusCode = await DeleteProductAsync(product.Id);
            //Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        Console.ReadLine();
    }
}

    [Authorize]
    class Validator : ApiController
    {
        static string page = "";
        public static void validate(string call)
        {
            
        }
    }
    */

    //[Authorize]
    //[RoutePrefix("api")]
    /// <summary>
    /// 
    /// </summary>
    public class IntelligentBillingAPIController : ApiController
    {
        private static readonly RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();
        private string generalErrorCode = "512";
        private string generalErrorMessage = "Server Error (Please contact Administrator).";
        private string notFoundErrorCode = "204";
        private string notFoundErrorMessage = "No data found.";
        private string defaultDate = "20150101";
        private string defaultExpiryDate = "20360101";
        private string defaultWildcardString = "@@@";
        private string defaultWildcardCreateString = "";
        private int defaultWildcardNumber = -2;
        private bool emptyErrors = true;//false;
        private bool errorJson = true;
        private bool emptyJson = false;


        /// <summary>
        /// 
        /// </summary>
        public string GenerateToken(string user)
        {
            int length = 240;
            string token = "";
            try
            {

                // We chose an encoding that fits 6 bits into every character,
                // so we can fit length*6 bits in total.
                // Each byte is 8 bits, so...
                int sufficientBufferSizeInBytes = (length * 6 + 7) / 8;

                var buffer = new byte[sufficientBufferSizeInBytes];
                random.GetBytes(buffer);
                token = Convert.ToBase64String(buffer).Substring(0, length);
                /*
                Microsoft.Owin.Security.AuthenticationTicket at = new Microsoft.Owin.Security.AuthenticationTicket(new ClaimsIdentity("Bearer", "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"),
                new Microsoft.Owin.Security.AuthenticationProperties
                {
                    AllowRefresh = true,
                    IsPersistent = true,
                    IssuedUtc = DateTime.UtcNow,
                    ExpiresUtc = DateTime.UtcNow.AddDays(1) // whenever you want your new token's expiration to happen
                });

                //// add any claims you want here like this:
                //Claim c = new Claim("user", "generic");
                //var claims = new List<Claim>();
                //claims.Add(new Claim(ClaimTypes.Name, "Brock"));
                //claims.Add(new Claim(ClaimTypes.Email, "brockallen@gmail.com"));
                //var id = new ClaimsIdentity(claims, Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ApplicationCookie);
                
                at.Identity.AddClaim(new Claim(ClaimTypes.Email, user));
                // and so on
                OAuthBearerAuthenticationOptions oabao = new OAuthBearerAuthenticationOptions();
                AuthenticationTicket ticket = AccessTokenFormat.Unprotect("");
                oabao.AccessTokenFormat = ticket;
                token = oabao.AccessTokenFormat.Protect(at);
                */
            }
            catch (Exception e)
            {

            }

            // You now have the token string in the token variable.
            return token;
        }
        /// <summary>
        /// 
        /// </summary>
        [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        [Route("api/Login")]
        public IEnumerable<APIResults_Login_Output> Login(APIResults_Login_Input search)
        {
            string UserName = "";
            string Password = "";
            /*  ClaimsIdentity p

              var oauthOptions = new OAuthAuthorizationServerOptions()
              {
                  TokenEndpointPath = new PathString("/Token"),
                  Provider = new SimpleAuthorizationServerProvider(),
                  AccessTokenFormat = new TicketDataFormat(app.CreateDataProtector(typeof(OAuthAuthorizationServerMiddleware).Namespace, "Access_Token", "v1")),
                  RefreshTokenFormat = new TicketDataFormat(app.CreateDataProtector(typeof(OAuthAuthorizationServerMiddleware).Namespace, "Refresh_Token", "v1")),
                  AccessTokenProvider = new AuthenticationTokenProvider(),
                  RefreshTokenProvider = new AuthenticationTokenProvider(),
                  AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                  AllowInsecureHttp = true
              };

              app.UseOAuthAuthorizationServer(oauthOptions);
              app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            */
            string Token = Guid.NewGuid().ToString();
            try
            {
                if (search != null)
                {
                    if (!String.IsNullOrEmpty(search.UserName))
                        UserName = search.UserName;
                    if (!String.IsNullOrEmpty(search.Password))
                        Password = search.Password;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    Token = GenerateToken(UserName);
                    SqlCommand com = new SqlCommand("AuthAPI_Login_Post", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.Int);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("UserName", SqlDbType.VarChar).Value = UserName;
                    com.Parameters.Add("Password", SqlDbType.VarChar).Value = Password;
                    com.Parameters.Add("Token", SqlDbType.VarChar).Value = Token;
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    List<APIResults_Login_Output> ret = dt.ToListCollection<APIResults_Login_Output>();
                    return ret.AsEnumerable<APIResults_Login_Output>();
                }
                catch (Exception e)
                {
                    return (new List<APIResults_Login_Output>()).AsEnumerable<APIResults_Login_Output>();
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<APIResults_Validate_Output> Validate(string token, string method)
        {
            APIResults_Validate_Input search = new APIResults_Validate_Input();
            search.LoginToken = token;
            search.MethodName = method;
            return Validate(search);
        }
        /// <summary>
        /// Validate Login Details
        /// </summary>
        public IEnumerable<APIResults_Validate_Output> Validate(APIResults_Validate_Input search)
        {


            string LoginToken = "";
            string MethodName = "";
            try
            {
                if (search != null)
                {
                    if (!String.IsNullOrEmpty(search.LoginToken))
                        LoginToken = search.LoginToken;
                    if (!String.IsNullOrEmpty(search.MethodName))
                        MethodName = search.MethodName;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {


                    SqlCommand com = new SqlCommand("AuthAPI_Validate_Post", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.Int);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("LoginToken", SqlDbType.VarChar).Value = LoginToken;
                    com.Parameters.Add("MethodName", SqlDbType.VarChar).Value = MethodName;
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    List<APIResults_Validate_Output> ret = dt.ToListCollection<APIResults_Validate_Output>();
                    return ret.AsEnumerable<APIResults_Validate_Output>();
                }
                catch (Exception e)
                {
                    return (new List<APIResults_Validate_Output>()).AsEnumerable<APIResults_Validate_Output>();
                }
            }
        }
        /*
        [Authorize]
        [HttpPost]
        public void ValidationFailed(APIResults_Validate_Input search)
        {


            string LoginToken = "";
            string MethodName = "";
            try
            {
                if (search != null)
                {
                    if (!String.IsNullOrEmpty(search.LoginToken))
                        LoginToken = search.LoginToken;
                    if (!String.IsNullOrEmpty(search.MethodName))
                        MethodName = search.MethodName;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //return (new List<APIResults_Validate_Output>()).AsEnumerable<APIResults_Validate_Output>();
        }
        */

        /// <summary>
        /// Error Output
        /// </summary>
        public IEnumerable<APIResults_Error_Output> genErrorResult_v1(string code, string message)
        {
            List<APIResults_Error_Output> errList = new List<APIResults_Error_Output>();
            APIResults_Error_Output err = new APIResults_Error_Output();
            err.Code = code;
            err.Message = message;
            errList.Add(err);
            return (errList.AsEnumerable<APIResults_Error_Output>());
        }

        /// <summary>
        /// Error Output
        /// </summary>
        public object genErrorResult(string code, string message)
        {
            List<APIResults_Error_Output> errList = new List<APIResults_Error_Output>();
            APIResults_Error_Output err = new APIResults_Error_Output();
            err.Code = code;
            err.Message = message;
            errList.Add(err);
            // return (errList.AsEnumerable<APIResults_Error_Output>());
            //return BadRequest(message);
            //return StatusCode(HttpStatusCode.PreconditionFailed);
            if (errorJson)
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, errList);
            return Request.CreateResponse(HttpStatusCode.PreconditionFailed, message);
            //return Request.CreateResponse(new System.Net.HttpStatusCode(460), message);
        }

        /// <summary>
        /// No data
        /// </summary>
        public object genEmptyResult(string code, string message)
        {
            List<APIResults_Error_Output> errList = new List<APIResults_Error_Output>();
            APIResults_Error_Output err = new APIResults_Error_Output();
            err.Code = code;
            err.Message = message;
            errList.Add(err);
            // return (errList.AsEnumerable<APIResults_Error_Output>());
            //return BadRequest(message);
            //return StatusCode(HttpStatusCode.PreconditionFailed);
            if (emptyJson)
                return Request.CreateResponse(HttpStatusCode.NoContent, errList);
            //return Request.CreateResponse(HttpStatusCode.NoContent, message);
            //return Request.CreateResponse(HttpStatusCode.NoContent, new APIResults_EmptyError_Output());
            return Request.CreateResponse(HttpStatusCode.OK, new APIResults_EmptyError_Output());
            //return Request.CreateResponse(new System.Net.HttpStatusCode(460), message);
        }

        private static string[] DateValidation(string date)
        {
            DateTime dt;

            bool isValid = DateTime.TryParseExact(
                date,
                "yyyyMMdd",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None,
                out dt);
            string[] codeMessage = new string[] { "437", "Date Format Incorrect" };
            //string pattern = @"20[0-9]{2}.[0-2]{2}.[0-3]{1}.[0-9]{1}";
            //Match m = Regex.Match(date, pattern, RegexOptions.IgnoreCase);
            //if (m.Success)
            if (isValid)
                codeMessage = new string[] { "200", "" };
            return codeMessage;
        }

        private static string[] AccountNumberValidation(string data)
        {
            int dataLen = data.Length;
            bool isValid = true;
            if (dataLen > 12)
                isValid = false;

            string[] codeMessage = new string[] { "432", "Account Number Incorrect" };
            //string pattern = @"20[0-9]{2}.[0-2]{2}.[0-3]{1}.[0-9]{1}";
            //Match m = Regex.Match(date, pattern, RegexOptions.IgnoreCase);
            //if (m.Success)
            if (isValid)
                codeMessage = new string[] { "200", "" };
            return codeMessage;
        }
        private static string[] InvoiceNumberValidation(string data)
        {
            int dataLen = data.Length;
            bool isValid = true;
            if (dataLen > 12)
                isValid = false;

            string[] codeMessage = new string[] { "433", "Invoice Number Incorrect" };
            //string pattern = @"C{1}S{1}P{1}[0-9]?{10}";
            //Match m = Regex.Match(date, pattern, RegexOptions.IgnoreCase);
            //if (m.Success)
            if (isValid)
                codeMessage = new string[] { "200", "" };
            return codeMessage;
        }
        private static string[] CustomerNameValidation(string data)
        {
            int dataLen = data.Length;
            bool isValid = true;
            if (dataLen > 50)
                isValid = false;

            string[] codeMessage = new string[] { "434", "Customer Name Incorrect" };
            //string pattern = @"C{1}S{1}P{1}[0-9]?{10}";
            //Match m = Regex.Match(date, pattern, RegexOptions.IgnoreCase);
            //if (m.Success)
            if (isValid)
                codeMessage = new string[] { "200", "" };
            return codeMessage;
        }
        private static string[] ProductValidation(string data)
        {
            int dataLen = data.Length;
            bool isValid = true;
            if (dataLen > 20)
                isValid = false;

            string[] codeMessage = new string[] { "435", "Product Code Incorrect" };
            //string pattern = @"C{1}S{1}P{1}[0-9]?{10}";
            //Match m = Regex.Match(date, pattern, RegexOptions.IgnoreCase);
            //if (m.Success)
            if (isValid)
                codeMessage = new string[] { "200", "" };
            return codeMessage;
        }
        private static string[] TelephoneNumberValidation(string data)
        {
            int dataLen = data.Length;
            bool isValid = true;
            if (dataLen > 30)
                isValid = false;

            string[] codeMessage = new string[] { "436", "Telephone Number Incorrect" };
            //string pattern = @"C{1}S{1}P{1}[0-9]?{10}";
            //Match m = Regex.Match(date, pattern, RegexOptions.IgnoreCase);
            //if (m.Success)
            if (isValid)
                codeMessage = new string[] { "200", "" };
            return codeMessage;
        }
        private static string[] PBDIDValidation(string data)
        {
            string[] codeMessage = new string[] { "438", "ID Incorrect" };
            int dataLen = data.Length;
            bool isValid = true;
            if (dataLen > 30)
                isValid = false;
            if (isValid) 
            { 
                //string pattern = @"C{1}S{1}P{1}[0-9]?{10}";
                //Match m = Regex.Match(date, pattern, RegexOptions.IgnoreCase);
                //if (m.Success)
                string pattern = @"[0-9]+";
                Match m = Regex.Match(data, pattern, RegexOptions.IgnoreCase);
                isValid = m.Success;
            }
            if (isValid)
                codeMessage = new string[] { "200", "" };
            return codeMessage;
        }
        private static string[] InvoiceDayValidation(string data)
        {
            bool isValid = true;
            int j = 0;
            try
            {
                j = Int32.Parse(data);
                if (j > 31)
                    isValid = false;
                if (j < 1)
                    isValid = false;

            }
            catch (FormatException e)
            {
                //Console.WriteLine(e.Message);
                isValid = false;
            }
            

            string[] codeMessage = new string[] { "434", "Invoice Day Incorrect" };
            //string pattern = @"C{1}S{1}P{1}[0-9]?{10}";
            //Match m = Regex.Match(date, pattern, RegexOptions.IgnoreCase);
            //if (m.Success)
            if (isValid)
                codeMessage = new string[] { "200", "" };
            return codeMessage;
        }
        private static string[] RateSchemeValidation(string data)
        {
            string[] codeMessage = new string[] { "PRDEC10080", "Rate scheme code invalid" };
            int dataLen = data.Length;
            bool isValid = true;
            if (dataLen > 10)
            {
                isValid = false;
                codeMessage = new string[] { "PRDEC10081", "Rate scheme code too long" };

            }


            if (isValid)
            {   
                //string pattern = @"C{1}S{1}P{1}[0-9]?{10}";
                //Match m = Regex.Match(date, pattern, RegexOptions.IgnoreCase);
                //if (m.Success)
                string pattern = @"[0-9,A-Z,a-z]+";
                Match m = Regex.Match(data, pattern, RegexOptions.IgnoreCase);
                isValid = m.Success;
            }
                

            if (isValid)
                codeMessage = new string[] { "200", "" };
            return codeMessage;
        }

        private static string[] AreaValidation(string data)
        {
            string[] codeMessage = new string[] { "PRDEC10070", "Area code invalid" };
            int dataLen = data.Length;
            bool isValid = true;
            if (dataLen > 20)
            {
                isValid = false;
                codeMessage = new string[] { "PRDEC10071", "Area code too long" };

            }


            if (isValid)
            {
                //string pattern = @"C{1}S{1}P{1}[0-9]?{10}";
                //Match m = Regex.Match(date, pattern, RegexOptions.IgnoreCase);
                //if (m.Success)
                string pattern = @"[0-9,A-Z,a-z]+";
                Match m = Regex.Match(data, pattern, RegexOptions.IgnoreCase);
                isValid = m.Success;
            }


            if (isValid)
                codeMessage = new string[] { "200", "" };
            return codeMessage;
        }

        private static string[] CountryValidation(string data)
        {
            string[] codeMessage = new string[] { "PRDEC10060", "Country code invalid" };
            int dataLen = data.Length;
            bool isValid = true;
            if (dataLen > 10)
            {
                isValid = false;
                codeMessage = new string[] { "PRDEC10061", "Country code too long" };

            }


            if (isValid)
            {
                //string pattern = @"C{1}S{1}P{1}[0-9]?{10}";
                //Match m = Regex.Match(date, pattern, RegexOptions.IgnoreCase);
                //if (m.Success)
                string pattern = @"[0-9,A-Z,a-z]+";
                Match m = Regex.Match(data, pattern, RegexOptions.IgnoreCase);
                isValid = m.Success;
            }


            if (isValid)
                codeMessage = new string[] { "200", "" };
            return codeMessage;
        }


        /* Remove for PREACT
        /// <summary>
        /// Retrieve all customer records (filtered by search parameters).
        /// </summary>
        [HttpPost]
        [Route("api/GetCustomerList2a")]
        public IEnumerable<object> GetCustList2(APIResults_CustList_Input search)
        {
            string DBConnectionString = "";
            string Code = "402";
            string Message = "Authorisation Failed";
            string CustomerName = "";
            string AccountNumber = "";
            string Date = "20000101";
            try
            {
                try
                {
                    IEnumerable<APIResults_Validate_Output> valRes = Validate(Request.Headers.Authorization.ToString(), "GetCustList2");
                    DBConnectionString = valRes.ElementAt(0).ConnectionString;
                    Code = valRes.ElementAt(0).Code;
                    Message = valRes.ElementAt(0).Message;
                }
                catch(Exception e1)
                {
                    return genErrorResult(Code, Message);
                }
                
                
                if (!Code.Equals("200"))
                {
                    return genErrorResult(Code, Message);
                }
                if (search != null)
                {
                    if (!String.IsNullOrEmpty(search.CustomerName))
                        CustomerName = search.CustomerName;
                    if (!String.IsNullOrEmpty(search.AccountNumber))
                        AccountNumber = search.AccountNumber;

                    if (!String.IsNullOrEmpty(search.Date))
                        Date = search.Date;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            using (SqlConnection con = DbUtil.GetConnection_Target(DBConnectionString))
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_GetCustomerList_Post", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.Int);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("AccountNumber", SqlDbType.VarChar).Value = AccountNumber;
                    com.Parameters.Add("CustomerName", SqlDbType.VarChar).Value = CustomerName;
                    com.Parameters.Add("Date", SqlDbType.VarChar).Value = Date;
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    List<APIResults_CustList_Output> ret = dt.ToListCollection<APIResults_CustList_Output>();
                    return ret.AsEnumerable<APIResults_CustList_Output>();
                }
                catch (Exception e)
                {
                    return genErrorResult("500", "Error encountered. Please contact administrator");
                }
            }
        }
        //string custList= "GetCustomerList";
        [HttpPost]
        [Route("api/GetCustomerList2")]
        public IEnumerable<object> GetCustList_cSec(APIResults_CustList_Input search)
        {
            string DBConnectionString = "";
            string Code = "402";
            string Message = "Authorisation Failed";
            string CustomerName = "";
            string AccountNumber = "";
            string Date = "20000101";
            try
            {
                try
                {
                    IEnumerable<APIResults_Validate_Output> valRes = Validate(Request.Headers.Authorization.ToString(), "GetCustomerList");
                    DBConnectionString = valRes.ElementAt(0).ConnectionString;
                    Code = valRes.ElementAt(0).Code;
                    Message = valRes.ElementAt(0).Message;
                }
                catch (Exception e1)
                {
                    return genErrorResult(Code, Message);
                }


                if (!Code.Equals("200"))
                {
                    return genErrorResult(Code, Message);
                }
                if (search != null)
                {
                    if (!String.IsNullOrEmpty(search.CustomerName))
                        CustomerName = search.CustomerName;
                    if (!String.IsNullOrEmpty(search.AccountNumber))
                        AccountNumber = search.AccountNumber;

                    if (!String.IsNullOrEmpty(search.Date))
                        Date = search.Date;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                  SqlCommand com = new SqlCommand("AuthAPI_GetCustomerList_Post", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.Int);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("AccountNumber", SqlDbType.VarChar).Value = AccountNumber;
                    com.Parameters.Add("CustomerName", SqlDbType.VarChar).Value = CustomerName;
                    com.Parameters.Add("Date", SqlDbType.VarChar).Value = Date;
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    List<APIResults_CustList_Output> ret = dt.ToListCollection<APIResults_CustList_Output>();
                    return ret.AsEnumerable<APIResults_CustList_Output>();
                }
                catch (Exception e)
                {
                    //return (new List<APIResults_CustList_Output>()).AsEnumerable<APIResults_CustList_Output>();
                    return genErrorResult("500", "Error encountered (. Please contact administrator");
                }
            }
        }


        /// <summary>
        /// Retrieve the customer details for the customers (filtered by input parameters).
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/GetCustomerDetails2")]
        public IEnumerable<object> GetCustomerDetails_cSec(APIResults_CustDetails_Input search)
        {
            string DBConnectionString = "";
            string Code = "402";
            string Message = "Authorisation Failed";
            string ParentAccountNumber = "";
            string AccountNumber = "";
            string Date = "20000101";
            try
            {
                try
                {
                    IEnumerable<APIResults_Validate_Output> valRes = Validate(Request.Headers.Authorization.ToString(), "GetCustomerDetails");
                    DBConnectionString = valRes.ElementAt(0).ConnectionString;
                    Code = valRes.ElementAt(0).Code;
                    Message = valRes.ElementAt(0).Message;
                }
                catch (Exception e1)
                {
                    return genErrorResult(Code, Message);
                }


                if (!Code.Equals("200"))
                {
                    return genErrorResult(Code, Message);
                }
                if (search != null)
                {
                    if (!String.IsNullOrEmpty(search.ParentAccountNumber))
                        ParentAccountNumber = search.ParentAccountNumber;
                    if (!String.IsNullOrEmpty(search.AccountNumber))
                        AccountNumber = search.AccountNumber;

                    if (!String.IsNullOrEmpty(search.Date))
                        Date = search.Date;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_GetCustomerDetails_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("ParentAccountNumber", SqlDbType.VarChar).Value = ParentAccountNumber;
                    com.Parameters.Add("AccountNumber", SqlDbType.VarChar).Value = AccountNumber;
                    com.Parameters.Add("Date", SqlDbType.VarChar).Value = Date;
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    List<APIResults_CustDetails_Output> ret = dt.ToListCollection<APIResults_CustDetails_Output>();
                    return ret.AsEnumerable<APIResults_CustDetails_Output>();
                }
                catch (Exception e)
                {
                    return (new List<APIResults_CustDetails_Output>()).AsEnumerable<APIResults_CustDetails_Output>();
                }
            }
        }

        /// <summary>
        /// Returns the list of products (filtered by input parameters).
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/GetProductList2")]
        public IEnumerable<object> GetProductDetails_cSec(APIResults_ProdList_Input search)
        {
            string DBConnectionString = "";
            string Code = "402";
            string Message = "Authorisation Failed";
            string Product = "";
            string Date = "20000101";
            string Invoice = "20000101";
            try
            {
                try
                {
                    IEnumerable<APIResults_Validate_Output> valRes = Validate(Request.Headers.Authorization.ToString(), "GetProductList");
                    DBConnectionString = valRes.ElementAt(0).ConnectionString;
                    Code = valRes.ElementAt(0).Code;
                    Message = valRes.ElementAt(0).Message;
                }
                catch (Exception e1)
                {
                    return genErrorResult(Code, Message);
                }


                if (!Code.Equals("200"))
                {
                    return genErrorResult(Code, Message);
                }
                if (search != null)
                {
                    if (!String.IsNullOrEmpty(search.Product))
                        Product = search.Product;
                    if (!String.IsNullOrEmpty(search.Date))
                        Date = search.Date;
                    if (!String.IsNullOrEmpty(search.Invoice))
                        Invoice = search.Invoice;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_GetProductList_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("ProductCode", SqlDbType.VarChar).Value = Product;
                    com.Parameters.Add("Date", SqlDbType.VarChar).Value = Date;
                    com.Parameters.Add("Invoice", SqlDbType.VarChar).Value = Invoice;
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    List<APIResults_ProdList_Output> ret = dt.ToListCollection<APIResults_ProdList_Output>();
                    return ret.AsEnumerable<APIResults_ProdList_Output>();
                }
                catch (Exception e)
                {
                    return (new List<APIResults_ProdList_Output>()).AsEnumerable<APIResults_ProdList_Output>();
                }
            }
        }
        */
        /*
                [HttpPost]
                [Route("api/Test1")]
                //[Route("api/GetTestResponse")]
                //public IEnumerable<object>  TestResponse(APIResults_CustList_Input search)
                /// <summary>
                /// GoCardless API
                /// </summary>
                /// <param name="search"></param>
                /// <returns></returns>
                public object HandleWebHook()
                {
                    var requestBody = Request.InputStream;
                    requestBody.Seek(0, System.IO.SeekOrigin.Begin);
                    var requestJson = new StreamReader(requestBody).ReadToEnd();

                    // We recommend storing your webhook endpoint secret in an environment variable
                    // for security
                    var secret = ConfigurationManager.AppSettings["GoCardlessWebhookSecret"];
                    var signaure = Request.Headers["Webhook-Signature"] ?? "";
                    try
                    {

                         foreach (Event event in WebhookParser.Parse(requestJson, secret, signature))
                         {
                             // Process the event
                             Console.WriteLine("Test");
                         }
                         catch(Exception e1)
                         {
                             return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                         }
                        Console.WriteLine("Test");
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                 }
        */
        // POST api/Account/ChangePassword

        [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        //[Route("")]
        //[Route("api/GetTestResponse")]
        //public IEnumerable<object>  TestResponse(APIResults_CustList_Input search)
        public object TestResponse()
        {
            //var json = JsonConvert.DeserializeObject<System.Json.JsonArray>(search2.ToString());
            object search2 = "";
            var obj = JsonConvert.DeserializeObject<APIResults_CustDetails_Input>(search2.ToString());
            //APIResults_CustDetails_Input search = (obj.A)[0].ToObject<APIResults_CustDetails_Input>();
            APIResults_CustDetails_Input search = (APIResults_CustDetails_Input)obj;
            string Code = "401";
            string Message = "Authorisation Failed";
            string ParentAccountNumber = "";
            string AccountNumber = "";
            string Date = "20000101";
            try
            {

                if (search != null)
                {
                    if (!String.IsNullOrEmpty(search.ParentAccountNumber))
                        ParentAccountNumber = search.ParentAccountNumber;
                    if (!String.IsNullOrEmpty(search.AccountNumber))
                        AccountNumber = search.AccountNumber;

                    if (!String.IsNullOrEmpty(search.Date))
                        Date = search.Date;

                    string[] codeMessage = DateValidation(Date);
                    Code = codeMessage[0];
                    Message = codeMessage[1];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (!Code.Equals("200"))
            {

                //List<IHttpActionResult> errList = new List<IHttpActionResult>();
                //IHttpActionResult err = BadRequest(Message);
                //err.Code = code;
                //err.Message = message;
                //errList.Add(err);
                //return (errList.AsEnumerable<IHttpActionResult>());
                //return errList;
                //return BadRequest(Message);
                return StatusCode(HttpStatusCode.NotAcceptable);
                //return ObjectResult(HttpStatusCode.Unauthorized, "Invalid token");
            }
            //List<IHttpActionResult> okList = new List<IHttpActionResult>();
            //IHttpActionResult ok = Ok();
            //err.Code = code;
            //err.Message = message;
            //okList.Add(ok);
            //return GetCustList_cSec2(search);
            return Ok(GetCustList_cSec3(search));
        }

        /// <summary>
        /// 
        /// </summary>
        public object GetCustList_cSec3(APIResults_CustDetails_Input search)
        {
            string Code = "401";
            string Message = "Authorisation Failed";
            string ParentAccountNumber = "";
            string AccountNumber = "";
            string Date = "20000101";
            try
            {

                if (search != null)
                {
                    if (!String.IsNullOrEmpty(search.ParentAccountNumber))
                        ParentAccountNumber = search.ParentAccountNumber;
                    if (!String.IsNullOrEmpty(search.AccountNumber))
                        AccountNumber = search.AccountNumber;

                    if (!String.IsNullOrEmpty(search.Date))
                        Date = search.Date;
                }

                string[] codeMessage = DateValidation(Date);
                Code = codeMessage[0];
                Message = codeMessage[1];
                if (!Code.Equals("200"))
                {
                    return genErrorResult(Code, Message);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_GetCustomerDetails_Test", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.Int);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("AccountNumber", SqlDbType.VarChar).Value = AccountNumber;
                    com.Parameters.Add("ParentAccountNumber", SqlDbType.VarChar).Value = ParentAccountNumber;
                    com.Parameters.Add("Date", SqlDbType.VarChar).Value = Date;
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    List<APIResults_CustDetails_Output_Test> ret = dt.ToListCollection<APIResults_CustDetails_Output_Test>();
                    return ret.AsEnumerable<APIResults_CustDetails_Output_Test>();
                }
                catch (Exception e)
                {
                    //return (new List<APIResults_CustList_Output>()).AsEnumerable<APIResults_CustList_Output>();
                    return genErrorResult("500", "Error encountered (Please contact administrator");
                }
            }
        }


        /// <summary>
        /// Retrieve all customer records (filtered by search parameters).
        /// </summary>
        [Authorize]
        [HttpPost]
        [Route("api/GetCustomerList")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object GetCustomerList_Validation(APIResults_CustList_Input search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;
            string Date = defaultDate;
            string CustomerName = "";
            string AccountNumber = "";
            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    if (!String.IsNullOrEmpty(search.CustomerName))
                        CustomerName = search.CustomerName;
                    codeMessage = CustomerNameValidation(CustomerName);
                    if (codeMessage[0].Equals("200"))
                    {
                        if (!String.IsNullOrEmpty(search.AccountNumber))
                            AccountNumber = search.AccountNumber;
                        codeMessage = AccountNumberValidation(AccountNumber);
                        if (codeMessage[0].Equals("200"))
                        {
                            if (!String.IsNullOrEmpty(search.Date))
                                Date = search.Date;
                            codeMessage = DateValidation(Date);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }
            search.AccountNumber = AccountNumber;
            search.CustomerName = CustomerName;
            search.Date = Date;
            IEnumerable<APIResults_CustList_Output> returnObj = (IEnumerable<APIResults_CustList_Output>)GetCustList_cSec2(search);
            if (returnObj.Count()==0 && emptyErrors)
                return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            return Ok(returnObj);
        }
        private object GetCustList_cSec2(APIResults_CustList_Input search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_GetCustomerList_Post", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.Int);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("AccountNumber", SqlDbType.VarChar).Value = search.AccountNumber;
                    com.Parameters.Add("CustomerName", SqlDbType.VarChar).Value = search.CustomerName;
                    com.Parameters.Add("Date", SqlDbType.VarChar).Value = search.Date;
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    List<APIResults_CustList_Output> ret = dt.ToListCollection<APIResults_CustList_Output>();
                    return ret.AsEnumerable<APIResults_CustList_Output>();
                }
                catch (Exception e)
                {
                    //return (new List<APIResults_CustList_Output>()).AsEnumerable<APIResults_CustList_Output>();
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }



        /// <summary>
        /// Retrieve the customer details for the customers (filtered by input parameters).
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>

        [Authorize]
        [HttpPost]
        [Route("api/GetCustomerDetails")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object GetCustomerDetails_Validation(APIResults_CustDetails_Input search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;
            string Date = defaultDate;
            string ParentAccountNumber = "";
            string AccountNumber = "";
            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    if (!String.IsNullOrEmpty(search.AccountNumber))
                        AccountNumber = search.AccountNumber;
                    codeMessage = AccountNumberValidation(AccountNumber);
                    if (codeMessage[0].Equals("200"))
                    {
                        if (!String.IsNullOrEmpty(search.ParentAccountNumber))
                            ParentAccountNumber = search.ParentAccountNumber;
                        codeMessage = AccountNumberValidation(AccountNumber);
                        if (codeMessage[0].Equals("200"))
                        {
                            if (!String.IsNullOrEmpty(search.Date))
                                Date = search.Date;
                            codeMessage = DateValidation(Date);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }
            search.AccountNumber = AccountNumber;
            search.ParentAccountNumber = ParentAccountNumber;
            search.Date = Date;
            IEnumerable<APIResults_CustDetails_Output_Extra> returnObj = (IEnumerable<APIResults_CustDetails_Output_Extra>)GetCustomerDetails_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
                return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            return Ok(returnObj);
        }
        private object GetCustomerDetails_cSec2(APIResults_CustDetails_Input search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_GetCustomerDetails_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("ParentAccountNumber", SqlDbType.VarChar).Value = search.ParentAccountNumber;
                    com.Parameters.Add("AccountNumber", SqlDbType.VarChar).Value = search.AccountNumber;
                    com.Parameters.Add("Date", SqlDbType.VarChar).Value = search.Date;
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    List<APIResults_CustDetails_Output_Extra> ret = dt.ToListCollection<APIResults_CustDetails_Output_Extra>();
                    return ret.AsEnumerable<APIResults_CustDetails_Output_Extra>();
                }
                catch (Exception e)
                {
                    //return (new List<APIResults_CustDetails_Output_Extra>()).AsEnumerable<APIResults_CustDetails_Output_Extra>();
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }

        /// <summary>
        /// Returns the list of products (filtered by input parameters).
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>

        [Authorize]
        [HttpPost]
        [Route("api/GetProductList")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object GetProductList_Validation(APIResults_ProdList_Input search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;
            string Date = defaultDate;
            string Product = "";
            string Invoice = "";
            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    if (!String.IsNullOrEmpty(search.Invoice))
                        Invoice = search.Invoice;
                    codeMessage = InvoiceNumberValidation(Invoice);
                    if (codeMessage[0].Equals("200"))
                    {
                        if (!String.IsNullOrEmpty(search.Product))
                            Product = search.Product;
                        codeMessage = ProductValidation(Product);
                        if (codeMessage[0].Equals("200"))
                        {
                            if (!String.IsNullOrEmpty(search.Date))
                                Date = search.Date;
                            codeMessage = DateValidation(Date);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }
            search.Invoice = Invoice;
            search.Product = Product;
            search.Date = Date;
            IEnumerable<APIResults_ProdList_Output> returnObj = (IEnumerable<APIResults_ProdList_Output>)GetProductDetails_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
                return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            return Ok(returnObj);
        }
        private object GetProductDetails_cSec2(APIResults_ProdList_Input search)
        {
           // string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_GetProductList_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("ProductCode", SqlDbType.VarChar).Value = search.Product;
                    com.Parameters.Add("Date", SqlDbType.VarChar).Value = search.Date;
                    com.Parameters.Add("Invoice", SqlDbType.VarChar).Value = search.Invoice;
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    List<APIResults_ProdList_Output> ret = dt.ToListCollection<APIResults_ProdList_Output>();
                    return ret.AsEnumerable<APIResults_ProdList_Output>();
                }
                catch (Exception e)
                {
                    //return (new List<APIResults_ProdList_Output>()).AsEnumerable<APIResults_ProdList_Output>();
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }


        /// <summary>
        /// Get product instances assigned to customer (filtered by input parameters). 
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("api/GetCustomerProductList")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object GetCustomerProductlist_Validation(APIResults_CustProdList_Input search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;
            string Date = defaultDate;
            string Product = "";
            string Customer = "";
            string Invoice = "";
            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    if (!String.IsNullOrEmpty(search.Customer))
                        Customer = search.Customer;
                    codeMessage = AccountNumberValidation(Customer);
                    if (codeMessage[0].Equals("200"))
                    {
                        if (!String.IsNullOrEmpty(search.Invoice))
                            Invoice = search.Invoice;
                        codeMessage = InvoiceNumberValidation(Invoice);
                        if (codeMessage[0].Equals("200"))
                        {
                            if (!String.IsNullOrEmpty(search.Product))
                                Product = search.Product;
                            codeMessage = ProductValidation(Product);
                            if (codeMessage[0].Equals("200"))
                            {
                                if (!String.IsNullOrEmpty(search.Date))
                                    Date = search.Date;
                                codeMessage = DateValidation(Date);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }
            search.Customer = Customer;
            search.Invoice = Invoice;
            search.Product = Product;
            search.Date = Date;
            IEnumerable<APIResults_CustProdList_Output> returnObj = (IEnumerable<APIResults_CustProdList_Output>)GetCustomerProductList_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
                return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            return Ok(returnObj);
        }
        private object GetCustomerProductList_cSec2(APIResults_CustProdList_Input search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_GetCustomerProductList_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("Customer", SqlDbType.VarChar).Value = search.Customer;
                    com.Parameters.Add("Invoice", SqlDbType.VarChar).Value = search.Invoice;
                    com.Parameters.Add("Product", SqlDbType.VarChar).Value = search.Product;
                    com.Parameters.Add("Date", SqlDbType.VarChar).Value = search.Date;
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    List<APIResults_CustProdList_Output> ret = dt.ToListCollection<APIResults_CustProdList_Output>();
                    return ret.AsEnumerable<APIResults_CustProdList_Output>();
                }
                catch (Exception e)
                {
                    //return (new List<APIResults_CustProdList_Output>()).AsEnumerable<APIResults_CustProdList_Output>();
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }


        /// <summary>
        /// Get product instances assigned to telephone number (filtered by input parameters). 
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>

        [Authorize]
        [HttpPost]
        [Route("api/GetTelephoneProductList")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object GetTelephoneProductList_Validation(APIResults_TelProdList_Input search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;
            string Date = defaultDate; ;
            string Product = "";
            string Telephone = "";
            string Invoice = "";
            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    if (!String.IsNullOrEmpty(search.Telephone))
                        Telephone = search.Telephone;
                    codeMessage = TelephoneNumberValidation(Telephone);
                    if (codeMessage[0].Equals("200"))
                    {
                        if (!String.IsNullOrEmpty(search.Invoice))
                            Invoice = search.Invoice;
                        codeMessage = InvoiceNumberValidation(Invoice);
                        if (codeMessage[0].Equals("200"))
                        {
                            if (!String.IsNullOrEmpty(search.Product))
                                Product = search.Product;
                            codeMessage = ProductValidation(Product);
                            if (codeMessage[0].Equals("200"))
                            {
                                if (!String.IsNullOrEmpty(search.Date))
                                    Date = search.Date;
                                codeMessage = DateValidation(Date);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }
            search.Invoice = Invoice;
            search.Product = Product;
            search.Date = Date;
            search.Telephone = Telephone;
            IEnumerable<APIResults_TelProdList_Output> returnObj = (IEnumerable<APIResults_TelProdList_Output>)GetTelephoneProductList_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
                return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            return Ok(returnObj);
        }
        private object GetTelephoneProductList_cSec2(APIResults_TelProdList_Input search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_GetTelephoneProductList_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("Telephone", SqlDbType.VarChar).Value = search.Telephone;
                    com.Parameters.Add("Invoice", SqlDbType.VarChar).Value = search.Invoice;
                    com.Parameters.Add("Product", SqlDbType.VarChar).Value = search.Product;
                    com.Parameters.Add("Date", SqlDbType.VarChar).Value = search.Date;
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    List<APIResults_TelProdList_Output> ret = dt.ToListCollection<APIResults_TelProdList_Output>();
                    return ret.AsEnumerable<APIResults_TelProdList_Output>();
                }
                catch (Exception e)
                {
                    //return (new List<APIResults_TelProdList_Output>()).AsEnumerable<APIResults_TelProdList_Output>();
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }


        /// <summary>
        /// Get invoices assigned to customer (filtered by input parameters). 
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>



        /// <summary>
        /// Get the list of telephone numbers assigned to a customer (filtered by input parameters). 
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>

        [Authorize]
        [HttpPost]
        [Route("api/GetCustomerTelephoneList")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object GetCustomerTelephoneList_Validation(APIResults_CustTelList_Input search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;
            string Date = defaultDate;
            string Customer = "";
            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                     if (!String.IsNullOrEmpty(search.Customer))
                         Customer = search.Customer;
                     codeMessage = AccountNumberValidation(Customer);
                     if (codeMessage[0].Equals("200"))
                     {
                         if (!String.IsNullOrEmpty(search.Date))
                             Date = search.Date;
                         codeMessage = DateValidation(Date);
                     }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }
            search.Customer = Customer;
            search.Date = Date;
            IEnumerable<APIResults_CustTelList_Output> returnObj = (IEnumerable<APIResults_CustTelList_Output>)GetCustomerTelephoneList_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
                return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            return Ok(returnObj);
        }
        private object GetCustomerTelephoneList_cSec2(APIResults_CustTelList_Input search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_GetCustomerTelephoneList_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("Customer", SqlDbType.VarChar).Value = search.Customer;
                    com.Parameters.Add("Date", SqlDbType.VarChar).Value = search.Date;
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    List<APIResults_CustTelList_Output> ret = dt.ToListCollection<APIResults_CustTelList_Output>();
                    return ret.AsEnumerable<APIResults_CustTelList_Output>();
                }
                catch (Exception e)
                {
                    //return (new List<APIResults_CustTelList_Output>()).AsEnumerable<APIResults_CustTelList_Output>();
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }

        /// <summary>
        /// Get the latest chargeable lines for a customer. 
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>

        [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
        [Authorize]
        [HttpPost]
        [Route("api/GetDailyCustomerProduct")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object GeDailyCustomerProduct_Validation(APIResults_DailyCustProd_Input search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;
            string Date = defaultDate;
            string Customer = "";
            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    if (!String.IsNullOrEmpty(search.Customer))
                        Customer = search.Customer;
                    codeMessage = AccountNumberValidation(Customer);
                    if (codeMessage[0].Equals("200"))
                    {
                        if (codeMessage[0].Equals("200"))
                        {
                            if (!String.IsNullOrEmpty(search.Date))
                                Date = search.Date;
                            codeMessage = DateValidation(Date);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }
            search.Customer = Customer;
            search.Date = Date;
            IEnumerable<APIResults_DailyCustProd_Output> returnObj = (IEnumerable<APIResults_DailyCustProd_Output>)GetDailyCustomerProduct_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
                return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            return Ok(returnObj);
        }
        private object GetDailyCustomerProduct_cSec2(APIResults_DailyCustProd_Input search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_GetDailyCustomerProduct_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("Customer", SqlDbType.VarChar).Value = search.Customer;
                    com.Parameters.Add("Date", SqlDbType.VarChar).Value = search.Date;
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    List<APIResults_DailyCustProd_Output> ret = dt.ToListCollection<APIResults_DailyCustProd_Output>();
                    return ret.AsEnumerable<APIResults_DailyCustProd_Output>();
                }
                catch (Exception e)
                {
                    //return (new List<APIResults_DailyCustProd_Output>()).AsEnumerable<APIResults_DailyCustProd_Output>();
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }




        /// <summary>
        /// Get the changes for chargeable lines for a customer since a specific date. 
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>

        [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
        [Authorize]
        [HttpPost]
        [Route("api/GetDailyCustomerProductChanges")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object GetDailyCustomerProductChanges_Validation(APIResults_DailyCustProdChanges_Input search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;
            string Date = defaultDate;
            string Customer = "";
            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    if (!String.IsNullOrEmpty(search.Customer))
                        Customer = search.Customer;
                    codeMessage = AccountNumberValidation(Customer);
                    if (codeMessage[0].Equals("200"))
                    {
                        if (codeMessage[0].Equals("200"))
                        {
                            if (!String.IsNullOrEmpty(search.Date))
                                Date = search.Date;
                            codeMessage = DateValidation(Date);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }
            search.Customer = Customer;
            search.Date = Date;
            IEnumerable<APIResults_DailyCustProdChanges_Output> returnObj = (IEnumerable<APIResults_DailyCustProdChanges_Output>)GetDailyCustomerProductChanges_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
                return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            return Ok(returnObj);
        }
        private object GetDailyCustomerProductChanges_cSec2(APIResults_DailyCustProdChanges_Input search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_GetDailyCustomerProductChanges_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("Customer", SqlDbType.VarChar).Value = search.Customer;
                    com.Parameters.Add("Date", SqlDbType.VarChar).Value = search.Date;
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    List<APIResults_DailyCustProdChanges_Output> ret = dt.ToListCollection<APIResults_DailyCustProdChanges_Output>();
                    return ret.AsEnumerable<APIResults_DailyCustProdChanges_Output>();
                }
                catch (Exception e)
                {
                    //return (new List<APIResults_DailyCustProdChanges_Output>()).AsEnumerable<APIResults_DailyCustProdChanges_Output>();
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }

        /*
                [HttpPost]
                [Route("api/GetCurrentProductStatus")]
                public IEnumerable<products> GetDetCurrentProductStatus(SearchData search)
                {
                    string ProductCode = "";
                    string Date = "20000101";
                    try
                    {
                        if (search != null)
                        {
                            if (!String.IsNullOrEmpty(search.ProductCode))
                                ProductCode = search.ProductCode;
                            if (!String.IsNullOrEmpty(search.Date))
                                Date = search.Date;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    using (SqlConnection con = DbUtil.GetConnection())
                    {
                        SqlCommand com = new SqlCommand("AuthAPI_GetProductList_POST", con);
                        com.CommandType = CommandType.StoredProcedure;
                        SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                        RetVal.Direction = ParameterDirection.ReturnValue;
                        com.Parameters.Add("ProductCode", SqlDbType.VarChar).Value = ProductCode;
                        com.Parameters.Add("Date", SqlDbType.VarChar).Value = Date;
                        SqlDataAdapter da = new SqlDataAdapter(com);
                        con.Open();
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        da.Dispose();
                        //      logger.Info("customers_get:, return={0}", RetVal.Value);
                        DataTable dt = ds.Tables[0];
                        List<products> ret = dt.ToListCollection<products>();
                        return ret.AsEnumerable<products>();
                    }
                }
                */
        private static string ip = "95.130.96.234";
    /// <summary>
    /// Get the Usage summary for a specific customer and date range. 
    /// </summary>
    /// <param name="search"></param>
    /// <returns>blah</returns>
    /// /// <Description>
    /// BBGet the Usage summary for a specific customer and date range. 
    /// </Description>
        [Authorize]
        [HttpPost]
        [Route("api/GetUsageSummary")]
        //[IPAddressFilter( "95.130.96.1", "95.130.96.256", IPAddressFilteringAction.Allow)]
        [IPAddressFilter("127.0.0.1",IPAddressFilteringAction.Allow)]
        //[IPAddressFilter(ip, IPAddressFilteringAction.Allow)]
        //[IPAddressFilter("::1", IPAddressFilteringAction.Allow)]
        public object GetUsageSummaryValidation(APIResults_UsageSummary_Input search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;
            string DateTo = defaultDate;
            string DateFrom = defaultDate;
            string AccountNumber = "";
            string Invoice = "0";
            string CLI = "";
            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {


                    if (!String.IsNullOrEmpty(search.AccountNumber))
                        AccountNumber = search.AccountNumber;
                    codeMessage = AccountNumberValidation(AccountNumber);
                    if (codeMessage[0].Equals("200"))
                    {
                        if (!String.IsNullOrEmpty(search.Invoice))
                            Invoice = search.Invoice;
                        codeMessage = AccountNumberValidation(Invoice);
                        if (codeMessage[0].Equals("200"))
                        {
                            //if (!String.IsNullOrEmpty(search.DateFrom))
                            //    DateFrom = search.DateFrom;
                            //codeMessage = DateValidation(DateFrom);
                            //if (codeMessage[0].Equals("200"))
                            //{
                            //    if (!String.IsNullOrEmpty(search.DateTo))
                            //        DateTo = search.DateTo;
                            //    codeMessage = DateValidation(DateTo);
                            //    if (codeMessage[0].Equals("200"))
                            //    {
                                    if (!String.IsNullOrEmpty(search.CLI))
                                        CLI = search.CLI;
                                    codeMessage = TelephoneNumberValidation(CLI);
                            //    }
                            //}
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }
            search.AccountNumber = AccountNumber;
            search.CLI = CLI;
            search.Invoice = Invoice;
            //search.DateFrom = DateFrom;
            //search.DateTo = DateTo;
            IEnumerable<APIResults_UsageSummary_Output> returnObj = (IEnumerable<APIResults_UsageSummary_Output>)GetUsageSummary_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
                return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            return Ok(returnObj);
        }
        private object GetUsageSummary_cSec2(APIResults_UsageSummary_Input search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_GetUsageSummary_Post", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.Int);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("AccountNumber", SqlDbType.VarChar).Value = search.AccountNumber;
                    com.Parameters.Add("Invoice", SqlDbType.VarChar).Value = search.Invoice;
                    com.Parameters.Add("CLI", SqlDbType.VarChar).Value = search.CLI;


                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    //com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    //com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    //com.Parameters.Add("DateFrom", SqlDbType.VarChar).Value = search.DateFrom;
                    //com.Parameters.Add("DateTo", SqlDbType.VarChar).Value = search.DateTo;
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    List<APIResults_UsageSummary_Output> ret = dt.ToListCollection<APIResults_UsageSummary_Output>();
                    return ret.AsEnumerable<APIResults_UsageSummary_Output>();
                }
                catch (Exception e)
                {
                    //return (new List<APIResults_CustList_Output>()).AsEnumerable<APIResults_CustList_Output>();
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }



        /// <summary>
        ///  Get List of customers
        /// </summary>
        /// <param name="search"></param>
        /// <returns>CustomerDetails</returns>
        [Authorize]
        [HttpPost]
        [Route("api/GetCustomers")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object GetCustomers_Validation(GetCustomers search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.AccountNumber))
                            search.AccountNumber = "";
                        codeMessage = AccountNumberValidation(search.AccountNumber);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.CustomerName))
                            search.CustomerName="";
                        codeMessage = CustomerNameValidation(search.CustomerName);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.InvoiceDay.ToString()))
                            search.InvoiceDay = 0;
                        //codeMessage = InvoiceDayValidation(search.InvoiceDay.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }

            IEnumerable<CustomerDetails> returnObj = (IEnumerable<CustomerDetails>)GetCustomers_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
                return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            return Ok(returnObj);
        }
        private object GetCustomers_cSec2(GetCustomers search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_GetCustomers_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("CustomerName", SqlDbType.VarChar).Value = search.CustomerName;
                    com.Parameters.Add("AccountNumber", SqlDbType.VarChar).Value = search.AccountNumber;
                    com.Parameters.Add("InvoiceDay", SqlDbType.Int).Value = search.InvoiceDay;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    List<CustomerDetails> ret = dt.ToListCollection<CustomerDetails>();
                    return ret.AsEnumerable<CustomerDetails>();
                }
                catch (Exception e)
                {
                    //return (new List<APIResults_CustDetails_Output_Extra>()).AsEnumerable<APIResults_CustDetails_Output_Extra>();
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }

        /// <summary>
        /// Create customer
        /// </summary>
        /// <param name="search"></param>
        /// <returns>Customer created</returns>
        [Authorize]
        [HttpPost]
        [Route("api/CreateCustomer")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object CreateCustomer_Validation(CreateCustomer search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Parent))
                            search.Parent = "";
                        //codeMessage = AccountNumberValidation(Customer);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.AccountNumber))
                            search.AccountNumber = "0";
                        codeMessage = AccountNumberValidation(search.AccountNumber);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.CustomerName))
                            search.CustomerName = "";
                        codeMessage = CustomerNameValidation(search.CustomerName);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.InvoiceGroup))
                            search.InvoiceGroup = "";
                        //codeMessage = InvoiceGroupValidation(search.InvoiceGroup);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.VATID))
                            search.VATID = "0";
                        //codeMessage = VATIDValidation(search.VATID);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.AddressLine1))
                            search.AddressLine1 = "";
                        //codeMessage = AddressLineValidation(search.AddressLine1);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.AddressLine2))
                            search.AddressLine2 = "";
                        //codeMessage = AddressLineValidation(search.AddressLine2);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.AddressLine3))
                            search.AddressLine3 = "";
                        //codeMessage = AddressLineValidation(search.AddressLine3);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.AddressLine4))
                            search.AddressLine4 = "";
                        //codeMessage = AddressLineValidation(search.AddressLine4);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.City))
                            search.City = "";
                        //codeMessage = AddressLineValidation(search.City);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.PostCode))
                            search.PostCode = "";
                        //codeMessage = PostCodeValidation(search.PostCode);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Country))
                            search.Country = "UK";
                        //codeMessage = PostCodeValidation(search.PostCode);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.RateScheme))
                            search.RateScheme = "*";
                        //codeMessage = RateSchemeValidation(search.RateScheme);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Currency))
                            search.Currency = "GBP";
                        //codeMessage = CurrencyValidation(search.Currency);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.CustomerManager))
                            search.CustomerManager = "";
                        codeMessage = CustomerNameValidation(search.CustomerManager);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Email))
                            search.Email = "";
                        //codeMessage = EmailValidation(search.Email);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.InvoiceDay.ToString()))
                            search.InvoiceDay = 1;
                        codeMessage = InvoiceDayValidation(search.InvoiceDay.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }
            object returnObj = CreateCustomer_cSec2(search);
            try
            {
                IEnumerable<CustomerDetails> returnObjValid = (IEnumerable<CustomerDetails>)returnObj;
                if (returnObjValid.Count() == 0 && emptyErrors)
                {
                    GetCustomers newSearch = new GetCustomers();
                    newSearch.AccountNumber = search.AccountNumber;
                    newSearch.CustomerName = search.CustomerName;
                    returnObj = (IEnumerable<CustomerDetails>)GetCustomers_cSec2(newSearch);
                    if (returnObjValid.Count() == 0 && emptyErrors)
                        return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
                    returnObj = returnObjValid;
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, (IEnumerable<APIResults_Error_Output>)returnObj);
            }
            /*
            IEnumerable<CustomerDetails> returnObj = (IEnumerable<CustomerDetails>)CreateCustomer_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
            {
                //return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
                GetCustomers newSearch = new GetCustomers();
                newSearch.AccountNumber = search.AccountNumber;
                newSearch.CustomerName = search.CustomerName;
                returnObj = (IEnumerable<CustomerDetails>)GetCustomers_cSec2(newSearch);
                if (returnObj.Count() == 0 && emptyErrors)
                    return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            }
            */   
            return Ok(returnObj);
        }
        private object CreateCustomer_cSec2(CreateCustomer search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_CreateCustomer_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("Parent", SqlDbType.VarChar).Value = search.Parent;
                    com.Parameters.Add("AccountNumber", SqlDbType.VarChar).Value = search.AccountNumber;
                    com.Parameters.Add("CustomerName", SqlDbType.VarChar).Value = search.CustomerName;
                    com.Parameters.Add("InvoiceGroup", SqlDbType.VarChar).Value = search.InvoiceGroup;
                    com.Parameters.Add("AddressLine1", SqlDbType.VarChar).Value = search.AddressLine1;
                    com.Parameters.Add("AddressLine2", SqlDbType.VarChar).Value = search.AddressLine2;
                    com.Parameters.Add("AddressLine3", SqlDbType.VarChar).Value = search.AddressLine3;
                    com.Parameters.Add("AddressLine4", SqlDbType.VarChar).Value = search.AddressLine4;
                    com.Parameters.Add("City", SqlDbType.VarChar).Value = search.City;
                    com.Parameters.Add("Country", SqlDbType.VarChar).Value = search.Country;
                    com.Parameters.Add("PostCode", SqlDbType.VarChar).Value = search.PostCode;
                    com.Parameters.Add("RateScheme", SqlDbType.VarChar).Value = search.RateScheme;
                    com.Parameters.Add("Currency", SqlDbType.VarChar).Value = search.Currency;
                    com.Parameters.Add("CustomerManager", SqlDbType.VarChar).Value = search.CustomerManager;
                    com.Parameters.Add("Email", SqlDbType.VarChar).Value = search.Email;
                    com.Parameters.Add("VATID", SqlDbType.VarChar).Value = search.VATID;
                    com.Parameters.Add("InvoiceDay", SqlDbType.Int).Value = search.InvoiceDay;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    DataTable dt = ds.Tables[0];
                    List<CustomerDetails> ret = new List<CustomerDetails>();
                    //List<APIResults_AddCustomer_Output> ret = dt.ToListCollection<APIResults_AddCustomer_Output>();
                    //return ret.AsEnumerable<CustomerDetails>();
                    if (dt.Columns[0].ColumnName == "Code" && dt.Columns[1].ColumnName == "Message")
                        return ((dt.ToListCollection<APIResults_Error_Output>()).AsEnumerable<APIResults_Error_Output>());
                    return ((dt.ToListCollection<CustomerDetails>()).AsEnumerable<CustomerDetails>());
                }
                catch (Exception e)
                {
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }

        /// <summary>
        /// Update customer record
        /// </summary>
        /// <param name="search"></param>
        /// <returns>Customer records updated</returns>
        [Authorize]
        [HttpPost]
        [Route("api/UpdateCustomer")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object UpdateCustomer_Validation(UpdateCustomer search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Parent))
                            search.Parent = defaultWildcardString;
                    }
                    //codeMessage = AccountNumberValidation(Customer);
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.AccountNumber))
                            search.AccountNumber = defaultWildcardString;
                        codeMessage = AccountNumberValidation(search.AccountNumber);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.CustomerName))
                            search.CustomerName = defaultWildcardString;
                        codeMessage = CustomerNameValidation(search.CustomerName);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.InvoiceGroup))
                            search.InvoiceGroup = defaultWildcardString;
                        //codeMessage = InvoiceGroupValidation(search.InvoiceGroup);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.VATID))
                            search.VATID = defaultWildcardString;
                        //codeMessage = VATIDValidation(search.VATID);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.AddressLine1))
                            search.AddressLine1 = defaultWildcardString;
                        //codeMessage = AddressLineValidation(search.AddressLine1);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.AddressLine2))
                            search.AddressLine2 = defaultWildcardString;
                        //codeMessage = AddressLineValidation(search.AddressLine2);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.AddressLine3))
                            search.AddressLine3 = defaultWildcardString;
                        //codeMessage = AddressLineValidation(search.AddressLine3);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.AddressLine4))
                            search.AddressLine4 = defaultWildcardString;
                        //codeMessage = AddressLineValidation(search.AddressLine4);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.City))
                            search.City = defaultWildcardString;
                        //codeMessage = AddressLineValidation(search.City);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.PostCode))
                            search.PostCode = defaultWildcardString;
                        //codeMessage = PostCodeValidation(search.PostCode);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.RateScheme))
                            search.RateScheme = defaultWildcardString;
                        //codeMessage = RateSchemeValidation(search.RateScheme);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Currency))
                            search.Currency = defaultWildcardString;
                        //codeMessage = CurrencyValidation(search.Currency);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.CustomerManager))
                            search.CustomerManager = defaultWildcardString;
                        //codeMessage = CustomerManagerValidation(search.CustomerManager);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Email))
                            search.Email = defaultWildcardString;
                        //codeMessage = EmailValidation(search.Email);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Country))
                            search.Country = defaultWildcardString;
                        //codeMessage = CountryValidation(search.Country);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.InvoiceDay.ToString()))
                            search.InvoiceDay = -999;
                        //codeMessage = InvoiceDayValidation(search.InvoiceDay.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }
            object returnObj = UpdateCustomer_cSec2(search);
            try
            {
                IEnumerable<CustomerDetails> returnObjValid = (IEnumerable<CustomerDetails>)returnObj;
                if (returnObjValid.Count() == 0 && emptyErrors)
                {
                    GetCustomers newSearch = new GetCustomers();
                    newSearch.AccountNumber = search.AccountNumber;
                    newSearch.CustomerName = search.CustomerName;
                    newSearch.InvoiceDay = search.InvoiceDay;
                    returnObj = (IEnumerable<CustomerDetails>)GetCustomers_cSec2(newSearch);
                    if (returnObjValid.Count() == 0 && emptyErrors)
                        return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
                    returnObj = returnObjValid;
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, (IEnumerable<APIResults_Error_Output>)returnObj);
            }
/*            IEnumerable<CustomerDetails> returnObj = (IEnumerable<CustomerDetails>)UpdateCustomer_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
            {
                //return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
                GetCustomers newSearch = new GetCustomers();
                newSearch.AccountNumber = search.AccountNumber;
                newSearch.CustomerName = search.CustomerName;
                returnObj = (IEnumerable<CustomerDetails>)GetCustomers_cSec2(newSearch);
                if (returnObj.Count() == 0 && emptyErrors)
                    return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            }*/
            return Ok(returnObj);
        }
        private object UpdateCustomer_cSec2(UpdateCustomer search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_UpdateCustomer_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("Parent", SqlDbType.VarChar).Value = search.Parent;
                    com.Parameters.Add("AccountNumber", SqlDbType.VarChar).Value = search.AccountNumber;
                    com.Parameters.Add("CustomerName", SqlDbType.VarChar).Value = search.CustomerName;
                    com.Parameters.Add("InvoiceGroup", SqlDbType.VarChar).Value = search.InvoiceGroup;
                    com.Parameters.Add("AddressLine1", SqlDbType.VarChar).Value = search.AddressLine1;
                    com.Parameters.Add("AddressLine2", SqlDbType.VarChar).Value = search.AddressLine2;
                    com.Parameters.Add("AddressLine3", SqlDbType.VarChar).Value = search.AddressLine3;
                    com.Parameters.Add("AddressLine4", SqlDbType.VarChar).Value = search.AddressLine4;
                    com.Parameters.Add("City", SqlDbType.VarChar).Value = search.City;
                    com.Parameters.Add("Country", SqlDbType.VarChar).Value = search.Country;
                    com.Parameters.Add("PostCode", SqlDbType.VarChar).Value = search.PostCode;
                    com.Parameters.Add("RateScheme", SqlDbType.VarChar).Value = search.RateScheme;
                    com.Parameters.Add("Currency", SqlDbType.VarChar).Value = search.Currency;
                    com.Parameters.Add("CustomerManager", SqlDbType.VarChar).Value = search.CustomerManager;
                    com.Parameters.Add("Email", SqlDbType.VarChar).Value = search.Email;
                    com.Parameters.Add("VATID", SqlDbType.VarChar).Value = search.VATID;
                    com.Parameters.Add("InvoiceDay", SqlDbType.Int).Value = search.InvoiceDay;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    //List<CustomerDetails> ret = dt.ToListCollection<CustomerDetails>();
                    //return ret.AsEnumerable<CustomerDetails>();
                    if (dt.Columns[0].ColumnName == "Code" && dt.Columns[1].ColumnName == "Message")
                        return ((dt.ToListCollection<APIResults_Error_Output>()).AsEnumerable<APIResults_Error_Output>());
                    return ((dt.ToListCollection<CustomerDetails>()).AsEnumerable<CustomerDetails>());
                }
                catch (Exception e)
                {
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/TerminateCustomer")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object TerminateCustomer_Validation(TerminateCustomer search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.AccountNumber))
                            search.AccountNumber = "";
                        codeMessage = AccountNumberValidation(search.AccountNumber);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.ExpiryDate))
                            search.ExpiryDate = "";
                        codeMessage = DateValidation(search.ExpiryDate);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }
            object returnObj = TerminateCustomer_cSec2(search);
            try
            {
                IEnumerable<CustomerDetails> returnObjValid = (IEnumerable<CustomerDetails>)returnObj;
                if (returnObjValid.Count() == 0 && emptyErrors)
                {
                    GetCustomers newSearch = new GetCustomers();
                    newSearch.AccountNumber = search.AccountNumber;
                    returnObj = (IEnumerable<CustomerDetails>)GetCustomers_cSec2(newSearch);
                    if (returnObjValid.Count() == 0 && emptyErrors)
                        return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
                    returnObj = returnObjValid;
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, (IEnumerable<APIResults_Error_Output>)returnObj);
            }
            return Ok(returnObj);
        }
        private object TerminateCustomer_cSec2(TerminateCustomer search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_TerminateCustomer_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("AccountNumber", SqlDbType.VarChar).Value = search.AccountNumber;
                    com.Parameters.Add("ExpiryDate", SqlDbType.VarChar).Value = search.ExpiryDate;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    DataTable dt = ds.Tables[0];
                    if (dt.Columns[0].ColumnName == "Code" && dt.Columns[1].ColumnName == "Message")
                        return ((dt.ToListCollection<APIResults_Error_Output>()).AsEnumerable<APIResults_Error_Output>());
                    return ((dt.ToListCollection<CustomerDetails>()).AsEnumerable<CustomerDetails>());
                }
                catch (Exception e)
                {
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }

        /// <summary>
        /// Get list of telephone numbers
        /// </summary>
        /// <param name="search"></param>
        /// <returns>Telephone</returns>
        [Authorize]
        [HttpPost]
        [Route("api/GetTelephones")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object GetTelephones_Validation(GetTelephones search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.AccountNumber))
                            search.AccountNumber = "";
                        codeMessage = AccountNumberValidation(search.AccountNumber);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.TelephoneNumber))
                            search.TelephoneNumber = "";
                        codeMessage = TelephoneNumberValidation(search.TelephoneNumber);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.RateScheme))
                            search.RateScheme = "";
                        //codeMessage = RateSchemeValidation(search.RateScheme);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Date))
                            search.Date = "";
                        //codeMessage = DateValidation(search.Date);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }

            IEnumerable<Telephone> returnObj = (IEnumerable<Telephone>)GetTelephones_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
                return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            return Ok(returnObj);
        }
        private object GetTelephones_cSec2(GetTelephones search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_GetTelephones_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("AccountNumber", SqlDbType.VarChar).Value = search.AccountNumber;
                    com.Parameters.Add("TelephoneNumber", SqlDbType.VarChar).Value = search.TelephoneNumber;
                    com.Parameters.Add("RateScheme", SqlDbType.VarChar).Value = search.RateScheme;
                    com.Parameters.Add("Date", SqlDbType.VarChar).Value = search.Date;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    List<Telephone> ret = dt.ToListCollection<Telephone>();
                    return ret.AsEnumerable<Telephone>();
                }
                catch (Exception e)
                {
                    //return (new List<APIResults_CustDetails_Output_Extra>()).AsEnumerable<APIResults_CustDetails_Output_Extra>();
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }

        /// <summary>
        /// Add telephone number 
        /// </summary>
        /// <param name="search"></param>
        /// <returns>Telephone</returns>
        [Authorize]
        [HttpPost]
        [Route("api/CreateTelephone")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object CreateTelephone_Validation(CreateTelephone search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.AccountNumber))
                            search.AccountNumber = "";
                        //codeMessage = AccountNumberValidation(search.AccountNumber);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.TelephoneNumber))
                            search.TelephoneNumber = "";
                        //codeMessage = TelephoneNumberValidation(search.TelephoneNumber);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.RateScheme))
                            search.RateScheme = "*";
                        //codeMessage = RateSchemeValidation(search.RateScheme);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.EffectiveDate))
                            search.EffectiveDate = defaultDate;
                        //codeMessage = DateValidation(search.EffectiveDate);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.ExpiryDate))
                            search.ExpiryDate = defaultExpiryDate;
                        //codeMessage = DateValidation(search.ExpiryDate);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }
            object returnObj = CreateTelephone_cSec2(search);
            try
            {
                IEnumerable<Telephone> returnObjValid = (IEnumerable<Telephone>)returnObj;
                if (returnObjValid.Count() == 0 && emptyErrors)
                {
                    GetTelephones newSearch = new GetTelephones();
                    newSearch.AccountNumber = search.AccountNumber;
                    newSearch.TelephoneNumber = search.TelephoneNumber;
                    newSearch.RateScheme = search.RateScheme;
                    newSearch.Date = search.EffectiveDate;
                    returnObj = (IEnumerable<Telephone>)GetTelephones_cSec2(newSearch);
                    if (returnObjValid.Count() == 0 && emptyErrors)
                        return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
                    returnObj = returnObjValid;
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, (IEnumerable<APIResults_Error_Output>)returnObj);
            }
            /*
            IEnumerable<Telephone> returnObj = (IEnumerable<Telephone>)CreateTelephone_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
            {
                GetTelephones newSearch = new GetTelephones();
                newSearch.AccountNumber = search.AccountNumber;
                newSearch.TelephoneNumber = search.TelephoneNumber;
                newSearch.RateScheme = search.RateScheme;
                newSearch.Date = search.EffectiveDate;
                returnObj = (IEnumerable<Telephone>)GetTelephones_cSec2(newSearch);
                if (returnObj.Count() == 0 && emptyErrors)
                    return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            }*/
            return Ok(returnObj);
        }
        private object CreateTelephone_cSec2(CreateTelephone search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_CreateTelephone_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("AccountNumber", SqlDbType.VarChar).Value = search.AccountNumber;
                    com.Parameters.Add("TelephoneNumber", SqlDbType.VarChar).Value = search.TelephoneNumber;
                    com.Parameters.Add("RateScheme", SqlDbType.VarChar).Value = search.RateScheme;
                    com.Parameters.Add("EffectiveDate", SqlDbType.VarChar).Value = search.EffectiveDate;
                    com.Parameters.Add("ExpiryDate", SqlDbType.VarChar).Value = search.ExpiryDate;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    //List<Telephone> ret = dt.ToListCollection<Telephone>();
                    //return ret.AsEnumerable<Telephone>();
                    if (dt.Columns[0].ColumnName == "Code" && dt.Columns[1].ColumnName == "Message")
                        return ((dt.ToListCollection<APIResults_Error_Output>()).AsEnumerable<APIResults_Error_Output>());
                    return ((dt.ToListCollection<Telephone>()).AsEnumerable<Telephone>());
                }
                catch (Exception e)
                {
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }
        /// <summary>
        /// Update telephone record
        /// </summary>
        /// <param name="search"></param>
        /// <returns>Telephone records updated</returns>
        [Authorize]
        [HttpPost]
        [Route("api/UpdateTelephone")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object UpdateTelephone_Validation(UpdateTelephone search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.AccountNumber))
                            search.AccountNumber = defaultWildcardString;
                        //codeMessage = AccountNumberValidation(search.AccountNumber);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.TelephoneNumber))
                            search.TelephoneNumber = defaultWildcardString;
                        //codeMessage = TelephoneNumberValidation(search.TelephoneNumber);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.RateScheme))
                            search.RateScheme = defaultWildcardString;
                        //codeMessage = RateSchemeValidation(search.RateScheme);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.EffectiveDate))
                            search.EffectiveDate = defaultWildcardString;
                        //codeMessage = DateValidation(search.EffectiveDate);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.ExpiryDate))
                            search.ExpiryDate = defaultWildcardString;
                       //codeMessage = DateValidation(search.ExpiryDate);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }
            object returnObj = UpdateTelephone_cSec2(search);
            try
            {
                IEnumerable<Telephone> returnObjValid = (IEnumerable<Telephone>)returnObj;
                if (returnObjValid.Count() == 0 && emptyErrors)
                {
                    GetTelephones newSearch = new GetTelephones();
                    newSearch.AccountNumber = search.AccountNumber;
                    newSearch.TelephoneNumber = search.TelephoneNumber;
                    newSearch.RateScheme = search.RateScheme;
                    newSearch.Date = search.EffectiveDate;
                    returnObj = (IEnumerable<Telephone>)GetTelephones_cSec2(newSearch);
                    if (returnObjValid.Count() == 0 && emptyErrors)
                        return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
                    returnObj = returnObjValid;
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, (IEnumerable<APIResults_Error_Output>)returnObj);
            }
            /*
            IEnumerable<Telephone> returnObj = (IEnumerable<Telephone>)UpdateTelephone_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
            {
                GetTelephones newSearch = new GetTelephones();
                newSearch.AccountNumber = search.AccountNumber;
                newSearch.TelephoneNumber = search.TelephoneNumber;
                newSearch.RateScheme = search.RateScheme;
                newSearch.Date = search.EffectiveDate;
                returnObj = (IEnumerable<Telephone>)GetTelephones_cSec2(newSearch);
                if (returnObj.Count() == 0 && emptyErrors)
                    return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            }*/
            return Ok(returnObj);
        }
        private object UpdateTelephone_cSec2(UpdateTelephone search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_UpdateTelephone_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("AccountNumber", SqlDbType.VarChar).Value = search.AccountNumber;
                    com.Parameters.Add("TelephoneNumber", SqlDbType.VarChar).Value = search.TelephoneNumber;
                    com.Parameters.Add("RateScheme", SqlDbType.VarChar).Value = search.RateScheme;
                    com.Parameters.Add("EffectiveDate", SqlDbType.VarChar).Value = search.EffectiveDate;
                    com.Parameters.Add("ExpiryDate", SqlDbType.VarChar).Value = search.ExpiryDate;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    //List<Telephone> ret = dt.ToListCollection<Telephone>();
                    //return ret.AsEnumerable<Telephone>();
                    if (dt.Columns[0].ColumnName == "Code" && dt.Columns[1].ColumnName == "Message")
                        return ((dt.ToListCollection<APIResults_Error_Output>()).AsEnumerable<APIResults_Error_Output>());
                    return ((dt.ToListCollection<Telephone>()).AsEnumerable<Telephone>());
                }
                catch (Exception e)
                {
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/TerminateTelephone")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object TerminateTelephone_Validation(TerminateTelephone search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.TelephoneNumber))
                            search.TelephoneNumber = "";
                        codeMessage = AccountNumberValidation(search.TelephoneNumber);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.ExpiryDate))
                            search.ExpiryDate = "";
                        codeMessage = DateValidation(search.ExpiryDate);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }
            object returnObj = TerminateTelephone_cSec2(search);
            try
            {
                IEnumerable<Telephone> returnObjValid = (IEnumerable<Telephone>)returnObj;
                if (returnObjValid.Count() == 0 && emptyErrors)
                {
                    GetTelephones newSearch = new GetTelephones();
                    newSearch.TelephoneNumber = search.TelephoneNumber;
                    returnObj = (IEnumerable<CustomerDetails>)GetTelephones_cSec2(newSearch);
                    if (returnObjValid.Count() == 0 && emptyErrors)
                        return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
                    returnObj = returnObjValid;
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, (IEnumerable<APIResults_Error_Output>)returnObj);
            }
            return Ok(returnObj);
        }
        private object TerminateTelephone_cSec2(TerminateTelephone search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_TerminateTelephone_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("TelephoneNumber", SqlDbType.VarChar).Value = search.TelephoneNumber;
                    com.Parameters.Add("ExpiryDate", SqlDbType.VarChar).Value = search.ExpiryDate;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    DataTable dt = ds.Tables[0];
                    if (dt.Columns[0].ColumnName == "Code" && dt.Columns[1].ColumnName == "Message")
                        return ((dt.ToListCollection<APIResults_Error_Output>()).AsEnumerable<APIResults_Error_Output>());
                    return ((dt.ToListCollection<Telephone>()).AsEnumerable<Telephone>());
                }
                catch (Exception e)
                {
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }


        /// <summary>
        /// Get list of Rate Schemes
        /// </summary>
        /// <param name="search"></param>
        /// <returns>RateScheme</returns>
        [Authorize]
        [HttpPost]
        [Route("api/GetRateSchemes")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object GetRateSchemes_Validation(GetRateSchemes search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.RateScheme))
                            search.RateScheme = "";
                        //codeMessage = RateSchemeValidation(search.RateScheme);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Description))
                            search.Description = "";
                        //codeMessage = DescriptionValidation(search.Description);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }

            IEnumerable<Rate_Scheme> returnObj = (IEnumerable<Rate_Scheme>)GetRateSchemes_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
                return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            return Ok(returnObj);
        }
        private object GetRateSchemes_cSec2(GetRateSchemes search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_GetRateSchemes_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("RateScheme", SqlDbType.VarChar).Value = search.RateScheme;
                    com.Parameters.Add("Description", SqlDbType.VarChar).Value = search.Description;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    List<Rate_Scheme> ret = dt.ToListCollection<Rate_Scheme>();
                    return ret.AsEnumerable<Rate_Scheme>();
                }
                catch (Exception e)
                {
                    //return (new List<APIResults_CustDetails_Output_Extra>()).AsEnumerable<APIResults_CustDetails_Output_Extra>();
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }

        /// <summary>
        /// Create a rate scheme
        /// </summary>
        /// <param name="search"></param>
        /// <returns>Rate Scheme</returns>
        [Authorize]
        [HttpPost]
        [Route("api/CreateRatescheme")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object CreateRatescheme_Validation(CreateRateScheme search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.RateScheme))
                            search.RateScheme = "*";
                        codeMessage = RateSchemeValidation(search.RateScheme);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Description))
                            search.Description = "";
                        //codeMessage = RateSchemeDescriptionValidation(search.Description);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }
            object returnObj = CreateRatescheme_cSec2(search);
            try
            {
                IEnumerable<Rate_Scheme> returnObjValid = (IEnumerable<Rate_Scheme>)returnObj;
                if (returnObjValid.Count() == 0 && emptyErrors)
                {
                    GetRateSchemes newSearch = new GetRateSchemes();
                    newSearch.RateScheme = search.RateScheme;
                    returnObj = (IEnumerable<Rate_Scheme>)GetRateSchemes_cSec2(newSearch);
                    if (returnObjValid.Count() == 0 && emptyErrors)
                        return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
                    returnObj = returnObjValid;
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, (IEnumerable<APIResults_Error_Output>)returnObj);
            }
            return Ok(returnObj);
        }

        private object CreateRatescheme_cSec2(CreateRateScheme search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_CreateRatescheme_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("RateScheme", SqlDbType.VarChar).Value = search.RateScheme;
                    com.Parameters.Add("Description", SqlDbType.VarChar).Value = search.Description;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    //List<Rate_Scheme> ret = dt.ToListCollection<Rate_Scheme>();
                    //return ret.AsEnumerable<Rate_Scheme>();
                    if (dt.Columns[0].ColumnName == "Code" && dt.Columns[1].ColumnName == "Message")
                        return ((dt.ToListCollection<APIResults_Error_Output>()).AsEnumerable<APIResults_Error_Output>());
                    return ((dt.ToListCollection<Rate_Scheme>()).AsEnumerable<Rate_Scheme>());
                }
                catch (Exception e)
                {
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }

        /// <summary>
        /// Update rate scheme description
        /// </summary>
        /// <param name="search"></param>
        /// <returns>rate scheme</returns>
        [Authorize]
        [HttpPost]
        [Route("api/UpdateRatescheme")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object UpdateRatescheme_Validation(UpdateRateScheme search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.RateScheme))
                            search.RateScheme = "*";
                        //codeMessage = RateSchemeValidation(search.RateScheme);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Description))
                            search.Description = defaultWildcardString;
                        //codeMessage = RateSchemeDescriptionValidation(search.Description);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }

            IEnumerable<Rate_Scheme> returnObj = (IEnumerable<Rate_Scheme>)UpdateRatescheme_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
            {
                GetRateSchemes newSearch = new GetRateSchemes();
                newSearch.RateScheme = search.RateScheme;
                returnObj = (IEnumerable<Rate_Scheme>)GetRateSchemes_cSec2(newSearch);
                if (returnObj.Count() == 0 && emptyErrors)
                    return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            }
            return Ok(returnObj);
        }
        private object UpdateRatescheme_cSec2(UpdateRateScheme search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_UpdateRatescheme_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("RateScheme", SqlDbType.VarChar).Value = search.RateScheme;
                    com.Parameters.Add("Description", SqlDbType.VarChar).Value = search.Description;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    //List<Rate_Scheme> ret = dt.ToListCollection<Rate_Scheme>();
                    //return ret.AsEnumerable<Rate_Scheme>();
                    if (dt.Columns[0].ColumnName == "Code" && dt.Columns[1].ColumnName == "Message")
                        return ((dt.ToListCollection<APIResults_Error_Output>()).AsEnumerable<APIResults_Error_Output>());
                    return ((dt.ToListCollection<Rate_Scheme>()).AsEnumerable<Rate_Scheme>());
                }
                catch (Exception e)
                {
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }

        /// <summary>
        /// Get list of Rates as per search parameters
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("api/GetRates")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object GetRates_Validation(GetRates search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.RateScheme))
                            search.RateScheme = "";
                        //codeMessage = RateSchemeValidation(search.RateScheme);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.DestinationArea))
                            search.DestinationArea = "";
                        //codeMessage = AreaValidation(search.DestinationArea);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.DestinationCountry))
                            search.DestinationCountry = "";
                        //codeMessage = CountryValidation(search.DestinationCountry);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Date))
                            search.Date = ""; //defaultDate;
                        //codeMessage = DateValidation(search.Date);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.RateID.ToString()))
                            search.RateID = 0;
                        //codeMessage = RateIDValidation(search.RateID);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }
            
            IEnumerable<Rate> returnObj = (IEnumerable<Rate>)GetRates_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
            {
                GetRates newSearch = new GetRates();
                newSearch.RateScheme = search.RateScheme;
                newSearch.DestinationArea = search.DestinationArea;
                newSearch.DestinationCountry = search.DestinationCountry;
                newSearch.Date = search.Date;
                returnObj = (IEnumerable<Rate>)GetRates_cSec2(newSearch);
                if (returnObj.Count() == 0 && emptyErrors)
                    return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            }
            return Ok(returnObj);
        }
        private object GetRates_cSec2(GetRates search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_GetRates_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("RateScheme", SqlDbType.VarChar).Value = search.RateScheme;
                    com.Parameters.Add("DestinationArea", SqlDbType.VarChar).Value = search.DestinationArea;
                    com.Parameters.Add("DestinationCountry", SqlDbType.VarChar).Value = search.DestinationCountry;
                    com.Parameters.Add("Date", SqlDbType.VarChar).Value = search.Date;
                    com.Parameters.Add("RateID", SqlDbType.Int).Value = search.RateID;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    List<Rate> ret = dt.ToListCollection<Rate>();
                    return ret.AsEnumerable<Rate>();
                }
                catch (Exception e)
                {
                    //return (new List<APIResults_CustDetails_Output_Extra>()).AsEnumerable<APIResults_CustDetails_Output_Extra>();
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }

        /// <summary>
        /// Get list of RateOverrides as per search parameters
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("api/GetRateOverrides")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object GetRateOverrides_Validation(GetRateOverrides search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.AccountNumber))
                            search.AccountNumber = "";
                        //codeMessage = RateSchemeValidation(search.RateScheme);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.DestinationArea))
                            search.DestinationArea = "*";
                        //codeMessage = AreaValidation(search.DestinationArea);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Prefix))
                            search.Prefix = "";
                        //codeMessage = CountryValidation(search.DestinationCountry);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }

            IEnumerable<RateOverride> returnObj = (IEnumerable<RateOverride>)GetRateOverrides_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
            {
                GetRateOverrides newSearch = new GetRateOverrides();
                newSearch.AccountNumber = search.AccountNumber;
                newSearch.DestinationArea = search.DestinationArea;
                newSearch.Prefix = search.Prefix;
                returnObj = (IEnumerable<RateOverride>)GetRateOverrides_cSec2(newSearch);
                if (returnObj.Count() == 0 && emptyErrors)
                    return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            }
            return Ok(returnObj);
        }
        private object GetRateOverrides_cSec2(GetRateOverrides search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_GetRateOverrides_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("AccountNumber", SqlDbType.VarChar).Value = search.AccountNumber;
                    com.Parameters.Add("DestinationArea", SqlDbType.VarChar).Value = search.DestinationArea;
                    com.Parameters.Add("Prefix", SqlDbType.VarChar).Value = search.Prefix;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    List<RateOverride> ret = dt.ToListCollection<RateOverride>();
                    return ret.AsEnumerable<RateOverride>();
                }
                catch (Exception e)
                {
                    //return (new List<APIResults_CustDetails_Output_Extra>()).AsEnumerable<APIResults_CustDetails_Output_Extra>();
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }

        /// <summary>
        /// Create rate Override
        /// </summary>
        /// <param name="search"></param>
        /// <returns>Rate</returns>
        [Authorize]
        [HttpPost]
        [Route("api/CreateRateOverride")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object CreateRateOverride_Validation(CreateRateOverride search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.AccountNumber))
                            search.AccountNumber = "";
                        //codeMessage = RateSchemeValidation(search.RateScheme);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.DestinationArea))
                            search.DestinationArea = "*";
                        //codeMessage = AreaValidation(search.DestinationArea);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Prefix))
                            search.Prefix = "";
                        //codeMessage = AreaValidation(search.DestinationArea);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.PeakPPM.ToString()))
                            search.PeakPPM = 0;
                        //codeMessage = RateValidation(search.PricePerMinute);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.OffPeakPPC.ToString()))
                            search.OffPeakPPC = 0;
                        //codeMessage = RateValidation(search.PricePerMinute);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.OffPeakPPM.ToString()))
                            search.OffPeakPPM = 0;
                        //codeMessage = RateValidation(search.PricePerMinute);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.OffPeakPPC.ToString()))
                            search.OffPeakPPC = 0;
                        //codeMessage = RateValidation(search.PricePerMinute);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.WeekendPPM.ToString()))
                            search.WeekendPPM = 0;
                        //codeMessage = RateValidation(search.PricePerCall);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.WeekendPPC.ToString()))
                            search.WeekendPPC = 0;
                        //codeMessage = RateValidation(search.PricePerCall);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Minimum))
                            search.Minimum = "";
                        //codeMessage = MinimumValidation(search.Minimum);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }
            object returnObj = CreateRateOverride_cSec2(search);
            try
            {
                IEnumerable<RateOverride> returnObjValid = (IEnumerable<RateOverride>)returnObj;
                if (returnObjValid.Count() == 0 && emptyErrors)
                {
                    GetRateOverrides newSearch = new GetRateOverrides();
                    newSearch.AccountNumber = search.AccountNumber;
                    newSearch.DestinationArea = search.DestinationArea;
                    newSearch.Prefix = search.Prefix;
                    returnObj = (IEnumerable<RateOverride>)GetRateOverrides_cSec2(newSearch);
                    if (returnObjValid.Count() == 0 && emptyErrors)
                        return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
                    returnObj = returnObjValid;
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, (IEnumerable<APIResults_Error_Output>)returnObj);
            }
            /*
            IEnumerable<Rate> returnObj = (IEnumerable<Rate>)CreateRate_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
            {
                GetRates newSearch = new GetRates();
                newSearch.RateScheme = search.RateScheme;
                newSearch.DestinationArea = search.DestinationArea;
                newSearch.DestinationCountry = search.DestinationCountry;
                newSearch.Date = search.EffectiveDate;
                returnObj = (IEnumerable<Rate>)GetRates_cSec2(newSearch);
                if (returnObj.Count() == 0 && emptyErrors)
                    return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            }*/
            return Ok(returnObj);
        }
        private object CreateRateOverride_cSec2(CreateRateOverride search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_CreateRateOverride_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("AccountNumber", SqlDbType.VarChar).Value = search.AccountNumber;
                    com.Parameters.Add("DestinationArea", SqlDbType.VarChar).Value = search.DestinationArea;
                    com.Parameters.Add("Prefix", SqlDbType.VarChar).Value = search.Prefix;
                    com.Parameters.Add("PeakPPM", SqlDbType.VarChar).Value = search.PeakPPM;
                    com.Parameters.Add("PeakPPC", SqlDbType.VarChar).Value = search.PeakPPC;
                    com.Parameters.Add("OffPeakPPM", SqlDbType.VarChar).Value = search.OffPeakPPM;
                    com.Parameters.Add("OffPeakPPC", SqlDbType.Decimal).Value = search.OffPeakPPC;
                    com.Parameters.Add("WeekendPPM", SqlDbType.Decimal).Value = search.WeekendPPM;
                    com.Parameters.Add("WeekendPPC", SqlDbType.Decimal).Value = search.WeekendPPC;
                    com.Parameters.Add("Minimum", SqlDbType.VarChar).Value = search.Minimum;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    //List<Rate> ret = dt.ToListCollection<Rate>();
                    //return ret.AsEnumerable<Rate>();
                    if (dt.Columns[0].ColumnName == "Code" && dt.Columns[1].ColumnName == "Message")
                        return ((dt.ToListCollection<APIResults_Error_Output>()).AsEnumerable<APIResults_Error_Output>());
                    return ((dt.ToListCollection<RateOverride>()).AsEnumerable<RateOverride>());
                }
                catch (Exception e)
                {
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }
        /// <summary>
        /// Update Rate Override record 
        /// </summary>
        /// <param name="search"></param>
        /// <returns>Override Rate updated</returns>
        [Authorize]
        [HttpPost]
        [Route("api/UpdateRateOverride")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object UpdateRateOverride_Validation(UpdateRateOverride search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.AccountNumber))
                            search.AccountNumber = defaultWildcardString;
                        //codeMessage = RateSchemeValidation(search.RateScheme);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.DestinationArea))
                            search.DestinationArea = defaultWildcardString;
                        //codeMessage = AreaValidation(search.DestinationArea);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Prefix))
                            search.Prefix = defaultWildcardString;
                        //codeMessage = CountryValidation(search.DestinationCountry);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.PeakPPM.ToString()))
                            search.PeakPPM = -999;
                        //codeMessage = RateValidation(search.PricePerMinute);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.PeakPPC.ToString()))
                            search.PeakPPC = -999;
                        //codeMessage = RateValidation(search.PricePerCall);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.OffPeakPPM.ToString()))
                            search.OffPeakPPM = -999;
                        //codeMessage = RateValidation(search.PricePerMinute);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.OffPeakPPC.ToString()))
                            search.OffPeakPPC = -999;
                        //codeMessage = RateValidation(search.PricePerCall);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.WeekendPPM.ToString()))
                            search.WeekendPPM = -999;
                        //codeMessage = RateValidation(search.PricePerMinute);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.WeekendPPC.ToString()))
                            search.WeekendPPC = -999;
                        //codeMessage = RateValidation(search.PricePerCall);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Minimum))
                            search.Minimum = defaultWildcardString;
                        //codeMessage = MinimumValidation(search.Minimum);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }
            object returnObj = UpdateRateOverride_cSec2(search);
            try
            {
                IEnumerable<RateOverride> returnObjValid = (IEnumerable<RateOverride>)returnObj;
                if (returnObjValid.Count() == 0 && emptyErrors)
                {
                    GetRateOverrides newSearch = new GetRateOverrides();
                    newSearch.AccountNumber = search.AccountNumber;
                    newSearch.DestinationArea = search.DestinationArea;
                    newSearch.Prefix = search.Prefix;
                    returnObj = (IEnumerable<RateOverride>)GetRateOverrides_cSec2(newSearch);
                    if (returnObjValid.Count() == 0 && emptyErrors)
                        return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
                    returnObj = returnObjValid;
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, (IEnumerable<APIResults_Error_Output>)returnObj);
            }
            /*
            IEnumerable<Rate> returnObj = (IEnumerable<Rate>)UpdateRate_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
            {
                GetRate newSearch = new GetRate();
                newSearch.RateID = search.RateID.GetValueOrDefault(0);
                returnObj = (IEnumerable<Rate>)GetRate_cSec2(newSearch);
                if (returnObj.Count() == 0 && emptyErrors)
                    return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            }*/
            return Ok(returnObj);
        }
        private object UpdateRateOverride_cSec2(UpdateRateOverride search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_UpdateRateOverride_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("AccountNumber", SqlDbType.VarChar).Value = search.AccountNumber;
                    com.Parameters.Add("DestinationArea", SqlDbType.VarChar).Value = search.DestinationArea;
                    com.Parameters.Add("Prefix", SqlDbType.VarChar).Value = search.Prefix;
                    com.Parameters.Add("PeakPPM", SqlDbType.Decimal).Value = search.PeakPPM;
                    com.Parameters.Add("PeakPPC", SqlDbType.Decimal).Value = search.PeakPPC;
                    com.Parameters.Add("OffPeakPPM", SqlDbType.Decimal).Value = search.OffPeakPPM;
                    com.Parameters.Add("OffPeakPPC", SqlDbType.Decimal).Value = search.OffPeakPPC;
                    com.Parameters.Add("WeekendPPM", SqlDbType.Decimal).Value = search.WeekendPPM;
                    com.Parameters.Add("WeekendPPC", SqlDbType.Decimal).Value = search.WeekendPPC;
                    com.Parameters.Add("Minimum", SqlDbType.VarChar).Value = search.Minimum;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    //List<Rate> ret = dt.ToListCollection<Rate>();
                    //return ret.AsEnumerable<Rate>();
                    if (dt.Columns[0].ColumnName == "Code" && dt.Columns[1].ColumnName == "Message")
                        return ((dt.ToListCollection<APIResults_Error_Output>()).AsEnumerable<APIResults_Error_Output>());
                    return ((dt.ToListCollection<RateOverride>()).AsEnumerable<RateOverride>());
                }
                catch (Exception e)
                {
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }
        /// <summary>
        /// Delete rate Override
        /// </summary>
        /// <param name="search"></param>
        /// <returns>Rate updated</returns>
        [Authorize]
        [HttpPost]
        [Route("api/DeleteRateOverride")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object DeleteRate_Validation(DeleteRateOverride search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.AccountNumber))
                            search.AccountNumber = defaultWildcardString;
                        //codeMessage = RateSchemeValidation(search.RateScheme);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.DestinationArea))
                            search.DestinationArea = defaultWildcardString;
                        //codeMessage = AreaValidation(search.DestinationArea);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Prefix))
                            search.Prefix = defaultWildcardString;
                        //codeMessage = CountryValidation(search.DestinationCountry);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }
            object returnObj = DeleteRateOverride_cSec2(search);
            try
            {
                IEnumerable<RateOverride> returnObjValid = (IEnumerable<RateOverride>)returnObj;
                if (returnObjValid.Count() == 0 && emptyErrors)
                {
                    GetRateOverrides newSearch = new GetRateOverrides();
                    newSearch.AccountNumber = search.AccountNumber;
                    newSearch.DestinationArea = search.DestinationArea;
                    newSearch.Prefix = search.Prefix;
                    returnObj = (IEnumerable<RateOverride>)GetRateOverrides_cSec2(newSearch);
                    if (returnObjValid.Count() == 0 && emptyErrors)
                        return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
                    returnObj = returnObjValid;
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, (IEnumerable<APIResults_Error_Output>)returnObj);
            }
            /*
            IEnumerable<Rate> returnObj = (IEnumerable<Rate>)UpdateRate_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
            {
                GetRate newSearch = new GetRate();
                newSearch.RateID = search.RateID.GetValueOrDefault(0);
                returnObj = (IEnumerable<Rate>)GetRate_cSec2(newSearch);
                if (returnObj.Count() == 0 && emptyErrors)
                    return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            }*/
            return Ok(returnObj);
        }
        private object DeleteRateOverride_cSec2(DeleteRateOverride search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_DeleteRateOverride_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("AccountNumber", SqlDbType.VarChar).Value = search.AccountNumber;
                    com.Parameters.Add("DestinationArea", SqlDbType.VarChar).Value = search.DestinationArea;
                    com.Parameters.Add("Prefix", SqlDbType.VarChar).Value = search.Prefix;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();

                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    //List<Rate> ret = dt.ToListCollection<Rate>();
                    //return ret.AsEnumerable<Rate>();
                    if (dt.Columns[0].ColumnName == "Code" && dt.Columns[1].ColumnName == "Message")
                        return ((dt.ToListCollection<APIResults_Error_Output>()).AsEnumerable<APIResults_Error_Output>());
                    return ((dt.ToListCollection<RateOverride>()).AsEnumerable<RateOverride>());
                }
                catch (Exception e)
                {
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }

/*
        /// <summary>
        /// Create rate Override
        /// </summary>
        /// <param name="search"></param>
        /// <returns>Rate</returns>
        [Authorize]
        [HttpPost]
        [Route("api/CreateRateOverrides")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object CreateRateOverrides_Validation(CreateRateOverrides search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.AccountNumber))
                            search.AccountNumber = "";
                        //codeMessage = RateSchemeValidation(search.RateScheme);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.AreaList))
                            search.AreaList = "*";
                        //codeMessage = AreaValidation(search.DestinationArea);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.PeakPPM.ToString()))
                            search.PeakPPM = 0;
                        //codeMessage = RateValidation(search.PricePerMinute);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.OffPeakPPC.ToString()))
                            search.OffPeakPPC = 0;
                        //codeMessage = RateValidation(search.PricePerMinute);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.OffPeakPPM.ToString()))
                            search.OffPeakPPM = 0;
                        //codeMessage = RateValidation(search.PricePerMinute);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.OffPeakPPC.ToString()))
                            search.OffPeakPPC = 0;
                        //codeMessage = RateValidation(search.PricePerMinute);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.WeekendPPM.ToString()))
                            search.WeekendPPM = 0;
                        //codeMessage = RateValidation(search.PricePerCall);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.WeekendPPC.ToString()))
                            search.WeekendPPC = 0;
                        //codeMessage = RateValidation(search.PricePerCall);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Minimum))
                            search.Minimum = "";
                        //codeMessage = MinimumValidation(search.Minimum);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }
            object returnObj = CreateRateOverrides_cSec2(search);
            try
            {
                IEnumerable<RateOverride> returnObjValid = (IEnumerable<RateOverride>)returnObj;
                if (returnObjValid.Count() == 0 && emptyErrors)
                {
                    GetRateOverrides newSearch = new GetRateOverrides();
                    newSearch.AccountNumber = search.AccountNumber;
                    newSearch.DestinationArea = search.AreaList;
                    returnObj = (IEnumerable<RateOverride>)GetRateOverrides_cSec2(newSearch);
                    if (returnObjValid.Count() == 0 && emptyErrors)
                        return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
                    returnObj = returnObjValid;
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, (IEnumerable<APIResults_Error_Output>)returnObj);
            }
            return Ok(returnObj);
        }
        private object CreateRateOverrides_cSec2(CreateRateOverrides search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_CreateRateOverrides_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("AccountNumber", SqlDbType.VarChar).Value = search.AccountNumber;
                    com.Parameters.Add("AreaList", SqlDbType.VarChar).Value = search.AreaList;
                    com.Parameters.Add("PeakPPM", SqlDbType.VarChar).Value = search.PeakPPM;
                    com.Parameters.Add("PeakPPC", SqlDbType.VarChar).Value = search.PeakPPC;
                    com.Parameters.Add("OffPeakPPM", SqlDbType.VarChar).Value = search.OffPeakPPM;
                    com.Parameters.Add("OffPeakPPC", SqlDbType.Decimal).Value = search.OffPeakPPC;
                    com.Parameters.Add("WeekendPPM", SqlDbType.Decimal).Value = search.WeekendPPM;
                    com.Parameters.Add("WeekendPPC", SqlDbType.Decimal).Value = search.WeekendPPC;
                    com.Parameters.Add("Minimum", SqlDbType.VarChar).Value = search.Minimum;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    //List<Rate> ret = dt.ToListCollection<Rate>();
                    //return ret.AsEnumerable<Rate>();
                    if (dt.Columns[0].ColumnName == "Code" && dt.Columns[1].ColumnName == "Message")
                        return ((dt.ToListCollection<APIResults_Error_Output>()).AsEnumerable<APIResults_Error_Output>());
                    return ((dt.ToListCollection<RateOverride>()).AsEnumerable<RateOverride>());
                }
                catch (Exception e)
                {
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }
        */
        /// <summary>
        /// Get Rate record for RateID
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("api/GetRate")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object GetRate_Validation(GetRate search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.RateID.ToString()))
                            search.RateID = 0;
                        //codeMessage = RateIDValidation(search.RateID);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }

            IEnumerable<Rate> returnObj = (IEnumerable<Rate>)GetRate_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
            {
                GetRate newSearch = new GetRate();
                newSearch.RateID = search.RateID;
                returnObj = (IEnumerable<Rate>)GetRate_cSec2(newSearch);
                if (returnObj.Count() == 0 && emptyErrors)
                    return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            }
            return Ok(returnObj);
        }
        private object GetRate_cSec2(GetRate search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_GetRate_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("RateID", SqlDbType.Int).Value = search.RateID;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    List<Rate> ret = dt.ToListCollection<Rate>();
                    return ret.AsEnumerable<Rate>();
                }
                catch (Exception e)
                {
                    //return (new List<APIResults_CustDetails_Output_Extra>()).AsEnumerable<APIResults_CustDetails_Output_Extra>();
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }


        /// <summary>
        /// Create rate
        /// </summary>
        /// <param name="search"></param>
        /// <returns>Rate</returns>
        [Authorize]
        [HttpPost]
        [Route("api/CreateRate")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object CreateRate_Validation(CreateRate search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.RateScheme))
                            search.RateScheme = "*";
                        //codeMessage = RateSchemeValidation(search.RateScheme);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.DestinationArea))
                            search.DestinationArea = "";
                        //codeMessage = AreaValidation(search.DestinationArea);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.DestinationCountry))
                            search.DestinationCountry = "";
                        //codeMessage = CountryValidation(search.DestinationCountry);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.CallType))
                            search.CallType = "";
                        //codeMessage = CallTypeValidation(search.CallType);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.ServiceClass))
                            search.ServiceClass = "";
                        //codeMessage = ServiceClassValidation(search.ServiceClass);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.TimeBand))
                            search.TimeBand = "";
                        //codeMessage = TimebandValidation(search.TimeBand);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.PricePerMinute.ToString()))
                            search.PricePerMinute = 0;
                        //codeMessage = RateValidation(search.PricePerMinute);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.PricePerCall.ToString()))
                            search.PricePerCall = 0;
                        //codeMessage = RateValidation(search.PricePerCall);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Minimum))
                            search.Minimum = "";
                        //codeMessage = MinimumValidation(search.Minimum);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Markup))
                            search.Markup = "";
                        //codeMessage = RateValidation(search.Markup);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.EffectiveDate))
                            search.EffectiveDate = defaultDate;
                        codeMessage = DateValidation(search.EffectiveDate);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.ExpiryDate))
                            search.ExpiryDate = defaultExpiryDate;
                        codeMessage = DateValidation(search.ExpiryDate);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }
            object returnObj = CreateRate_cSec2(search);
            try
            {
                IEnumerable<Rate> returnObjValid = (IEnumerable<Rate>)returnObj;
                if (returnObjValid.Count() == 0 && emptyErrors)
                {
                    GetRates newSearch = new GetRates();
                    newSearch.RateScheme = search.RateScheme;
                    newSearch.DestinationArea = search.DestinationArea;
                    newSearch.DestinationCountry = search.DestinationCountry;
                    newSearch.Date = search.EffectiveDate;
                    returnObj = (IEnumerable<Rate>)GetRates_cSec2(newSearch);
                    if (returnObjValid.Count() == 0 && emptyErrors)
                        return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
                    returnObj = returnObjValid;
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, (IEnumerable<APIResults_Error_Output>)returnObj);
            }
            /*
            IEnumerable<Rate> returnObj = (IEnumerable<Rate>)CreateRate_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
            {
                GetRates newSearch = new GetRates();
                newSearch.RateScheme = search.RateScheme;
                newSearch.DestinationArea = search.DestinationArea;
                newSearch.DestinationCountry = search.DestinationCountry;
                newSearch.Date = search.EffectiveDate;
                returnObj = (IEnumerable<Rate>)GetRates_cSec2(newSearch);
                if (returnObj.Count() == 0 && emptyErrors)
                    return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            }*/
            return Ok(returnObj);
        }
        private object CreateRate_cSec2(CreateRate search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_CreateRate_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("RateScheme", SqlDbType.VarChar).Value = search.RateScheme;
                    com.Parameters.Add("DestinationArea", SqlDbType.VarChar).Value = search.DestinationArea;
                    com.Parameters.Add("DestinationCountry", SqlDbType.VarChar).Value = search.DestinationCountry;
                    com.Parameters.Add("CallType", SqlDbType.VarChar).Value = search.CallType;
                    com.Parameters.Add("ServiceClass", SqlDbType.VarChar).Value = search.ServiceClass;
                    com.Parameters.Add("TimeBand", SqlDbType.VarChar).Value = search.TimeBand;
                    com.Parameters.Add("PricePerMinute", SqlDbType.Decimal).Value = search.PricePerMinute;
                    com.Parameters.Add("PricePerCall", SqlDbType.Decimal).Value = search.PricePerCall;
                    com.Parameters.Add("Minimum", SqlDbType.VarChar).Value = search.Minimum;
                    com.Parameters.Add("Markup", SqlDbType.VarChar).Value = search.Markup;
                    com.Parameters.Add("EffectiveDate", SqlDbType.VarChar).Value = search.EffectiveDate;
                    com.Parameters.Add("ExpiryDate", SqlDbType.VarChar).Value = search.ExpiryDate;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    //List<Rate> ret = dt.ToListCollection<Rate>();
                    //return ret.AsEnumerable<Rate>();
                    if (dt.Columns[0].ColumnName == "Code" && dt.Columns[1].ColumnName == "Message")
                        return ((dt.ToListCollection<APIResults_Error_Output>()).AsEnumerable<APIResults_Error_Output>());
                    return ((dt.ToListCollection<Rate>()).AsEnumerable<Rate>());
                }
                catch (Exception e)
                {
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }

        /// <summary>
        /// Update Rate record using Rate ID as identifier
        /// </summary>
        /// <param name="search"></param>
        /// <returns>Rate updated</returns>
        [Authorize]
        [HttpPost]
        [Route("api/UpdateRate")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object UpdateRate_Validation(UpdateRate search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.RateScheme))
                            search.RateScheme = defaultWildcardString;
                        //codeMessage = RateSchemeValidation(search.RateScheme);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.DestinationArea))
                            search.DestinationArea = defaultWildcardString;
                        //codeMessage = AreaValidation(search.DestinationArea);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.DestinationCountry))
                            search.DestinationCountry = defaultWildcardString;
                        //codeMessage = CountryValidation(search.DestinationCountry);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.CallType))
                            search.CallType = defaultWildcardString;
                        //codeMessage = CallTypeValidation(search.CallType);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.ServiceClass))
                            search.ServiceClass = defaultWildcardString;
                        //codeMessage = ServiceClassValidation(search.ServiceClass);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.TimeBand))
                            search.TimeBand = defaultWildcardString;
                        //codeMessage = TimebandValidation(search.TimeBand);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.PricePerMinute.ToString()))
                            search.PricePerMinute = -999;
                        //codeMessage = RateValidation(search.PricePerMinute);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.PricePerCall.ToString()))
                            search.PricePerCall = -999;
                        //codeMessage = RateValidation(search.PricePerCall);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Minimum))
                            search.Minimum = defaultWildcardString;
                        //codeMessage = MinimumValidation(search.Minimum);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Markup))
                            search.Markup = defaultWildcardString;
                        //codeMessage = RateValidation(search.Markup);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.EffectiveDate))
                            search.EffectiveDate = defaultDate;
                        codeMessage = DateValidation(search.EffectiveDate);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.ExpiryDate))
                            search.ExpiryDate = defaultExpiryDate;
                        codeMessage = DateValidation(search.ExpiryDate);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }
            object returnObj = UpdateRate_cSec2(search);
            try
            {
                IEnumerable<Rate> returnObjValid = (IEnumerable<Rate>)returnObj;
                if (returnObjValid.Count() == 0 && emptyErrors)
                {
                    GetRates newSearch = new GetRates();
                    newSearch.RateScheme = search.RateScheme;
                    newSearch.DestinationArea = search.DestinationArea;
                    newSearch.DestinationCountry = search.DestinationCountry;
                    newSearch.Date = search.EffectiveDate;
                    returnObj = (IEnumerable<Rate>)GetRates_cSec2(newSearch);
                    if (returnObjValid.Count() == 0 && emptyErrors)
                        return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
                    returnObj = returnObjValid;
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, (IEnumerable<APIResults_Error_Output>)returnObj);
            }
            /*
            IEnumerable<Rate> returnObj = (IEnumerable<Rate>)UpdateRate_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
            {
                GetRate newSearch = new GetRate();
                newSearch.RateID = search.RateID.GetValueOrDefault(0);
                returnObj = (IEnumerable<Rate>)GetRate_cSec2(newSearch);
                if (returnObj.Count() == 0 && emptyErrors)
                    return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            }*/
            return Ok(returnObj);
        }
        private object UpdateRate_cSec2(UpdateRate search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_UpdateRate_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("RateID", SqlDbType.Int).Value = search.RateID;
                    com.Parameters.Add("RateScheme", SqlDbType.VarChar).Value = search.RateScheme;
                    com.Parameters.Add("DestinationArea", SqlDbType.VarChar).Value = search.DestinationArea;
                    com.Parameters.Add("DestinationCountry", SqlDbType.VarChar).Value = search.DestinationCountry;
                    com.Parameters.Add("CallType", SqlDbType.VarChar).Value = search.CallType;
                    com.Parameters.Add("ServiceClass", SqlDbType.VarChar).Value = search.ServiceClass;
                    com.Parameters.Add("TimeBand", SqlDbType.VarChar).Value = search.TimeBand;
                    com.Parameters.Add("PricePerMinute", SqlDbType.Decimal).Value = search.PricePerMinute;
                    com.Parameters.Add("PricePerCall", SqlDbType.Decimal).Value = search.PricePerCall;
                    com.Parameters.Add("Minimum", SqlDbType.VarChar).Value = search.Minimum;
                    com.Parameters.Add("Markup", SqlDbType.VarChar).Value = search.Markup;
                    com.Parameters.Add("EffectiveDate", SqlDbType.VarChar).Value = search.EffectiveDate;
                    com.Parameters.Add("ExpiryDate", SqlDbType.VarChar).Value = search.ExpiryDate;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    //List<Rate> ret = dt.ToListCollection<Rate>();
                    //return ret.AsEnumerable<Rate>();
                    if (dt.Columns[0].ColumnName == "Code" && dt.Columns[1].ColumnName == "Message")
                        return ((dt.ToListCollection<APIResults_Error_Output>()).AsEnumerable<APIResults_Error_Output>());
                    return ((dt.ToListCollection<Rate>()).AsEnumerable<Rate>());
                }
                catch (Exception e)
                {
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }

        /// <summary>
        /// Get List of products
        /// </summary>
        /// <param name="search"></param>
        /// <returns>Product</returns>
        [Authorize]
        [HttpPost]
        [Route("api/GetProducts")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object GetProducts_Validation(GetProducts search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.ProductCode))
                            search.ProductCode = "";
                        //codeMessage = ProductCodeValidation(search.ProductCode);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Description))
                            search.Description = "";
                        //codeMessage = DescriptionValidation(search.Description);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.ProductGroup))
                            search.ProductGroup = "";
                        //codeMessage = ProductGroupValidation(search.ProductGroup);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Period))
                            search.Period = "";
                        //codeMessage = PeriodValidation(search.Period);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.ProductType))
                            search.ProductType = "";
                        //codeMessage = ProductTypeValidation(search.ProductType);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Date))
                            search.Date = defaultDate;
                        //codeMessage = DateValidation(search.Date);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Date))
                            search.Date = defaultDate;
                        codeMessage = DateValidation(search.Date);
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }

            IEnumerable<Product> returnObj = (IEnumerable<Product>)GetProducts_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
                return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            return Ok(returnObj);
        }
        private object GetProducts_cSec2(GetProducts search)
        {
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_GetProducts_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("ProductCode", SqlDbType.VarChar).Value = search.ProductCode;
                    com.Parameters.Add("ProductGroup", SqlDbType.VarChar).Value = search.ProductGroup;
                    com.Parameters.Add("Description", SqlDbType.VarChar).Value = search.Description;
                    com.Parameters.Add("Period", SqlDbType.VarChar).Value = search.Period;
                    com.Parameters.Add("ProductType", SqlDbType.VarChar).Value = search.ProductType;
                    com.Parameters.Add("Date", SqlDbType.VarChar).Value = search.Date;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    List<Product> ret = dt.ToListCollection<Product>();
                    return ret.AsEnumerable<Product>();
                }
                catch (Exception e)
                {
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }

        /// <summary>
        /// Create new product
        /// </summary>
        /// <param name="search"></param>
        /// <returns>Product</returns>
        [Authorize]
        [HttpPost]
        [Route("api/CreateProduct")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object CreateProduct_Validation(CreateProduct search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.ProductCode))
                            search.ProductCode = "";
                        //codeMessage = ProductCodeValidation(search.ProductCode);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Description))
                            search.Description = "";
                        //codeMessage = DescriptionValidation(search.Description);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.ProductGroup))
                            search.ProductGroup = "";
                        //codeMessage = ProductGroupValidation(search.ProductGroup);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Period))
                            search.Period = "2";
                        //codeMessage = PeriodValidation(search.Period);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.ProductType))
                            search.ProductType = "1";
                        //codeMessage = ProductTypeValidation(search.ProductType);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.MonthsInAdvance.ToString()))
                            search.MonthsInAdvance = 0;
                        //codeMessage = MonthsInAdvanceValidation(search.MonthsInAdvance);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.DefaultBuyPrice.ToString()))
                            search.DefaultBuyPrice = 0;
                        //codeMessage = DefaultBuyPriceValidation(search.DefaultBuyPrice);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.DefaultSellPrice.ToString()))
                            search.DefaultSellPrice = 0;
                        //codeMessage = DefaultSellPriceValidation(search.DefaultSellPrice);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }
            object returnObj = CreateProduct_cSec2(search);
            try
            {
                IEnumerable<Product> returnObjValid = (IEnumerable<Product>)returnObj;
                if (returnObjValid.Count() == 0 && emptyErrors)
                {
                    GetProducts newSearch = new GetProducts();
                    newSearch.ProductCode = search.ProductCode;
                    returnObj = (IEnumerable<Product>)GetProducts_cSec2(newSearch);
                    if (returnObjValid.Count() == 0 && emptyErrors)
                        return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
                    returnObj = returnObjValid;
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, (IEnumerable<APIResults_Error_Output>)returnObj);
            }
            /*
            IEnumerable<Product> returnObj = (IEnumerable<Product>)CreateProduct_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
            {
                //return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
                GetProducts newSearch = new GetProducts();
                newSearch.ProductCode = search.ProductCode;
                newSearch.Description = "";
                newSearch.ProductGroup = "";
                newSearch.Period = "";
                newSearch.ProductType = "";
                newSearch.Date = defaultDate;
                returnObj = (IEnumerable<Product>)GetProducts_cSec2(newSearch);
                if (returnObj.Count() == 0 && emptyErrors)
                    return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            }*/
            return Ok(returnObj);
        }
        private object CreateProduct_cSec2(CreateProduct search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_CreateProduct_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("ProductCode", SqlDbType.VarChar).Value = search.ProductCode;
                    com.Parameters.Add("ProductGroup", SqlDbType.VarChar).Value = search.ProductGroup;
                    com.Parameters.Add("Description", SqlDbType.VarChar).Value = search.Description;
                    com.Parameters.Add("Period", SqlDbType.VarChar).Value = search.Period;
                    com.Parameters.Add("ProductType", SqlDbType.VarChar).Value = search.ProductType;
                    com.Parameters.Add("DefaultSellPrice", SqlDbType.Decimal).Value = search.DefaultSellPrice;
                    com.Parameters.Add("DefaultBuyPrice", SqlDbType.Decimal).Value = search.DefaultBuyPrice;
                    com.Parameters.Add("MonthsInAdvance", SqlDbType.Int).Value = search.MonthsInAdvance;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    //List<Product> ret = dt.ToListCollection<Product>();
                    //return ret.AsEnumerable<Product>();
                    if (dt.Columns[0].ColumnName == "Code" && dt.Columns[1].ColumnName == "Message")
                        return ((dt.ToListCollection<APIResults_Error_Output>()).AsEnumerable<APIResults_Error_Output>());
                    return ((dt.ToListCollection<Product>()).AsEnumerable<Product>());
                }
                catch (Exception e)
                {
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }

        /// <summary>
        /// Update product record using productcode as identifier
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("api/UpdateProduct")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object UpdateProduct_Validation(UpdateProduct search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.ProductCode))
                            search.ProductCode = "";
                        //codeMessage = ProductCodeValidation(search.ProductCode);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Description))
                            search.Description = defaultWildcardString;
                        //codeMessage = DescriptionValidation(search.Description);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.ProductGroup))
                            search.ProductGroup = defaultWildcardString;
                        //codeMessage = ProductGroupValidation(search.ProductGroup);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Period))
                            search.Period = defaultWildcardString;
                        //codeMessage = PeriodValidation(search.Period);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.ProductType))
                            search.ProductType = defaultWildcardString;
                        //codeMessage = ProductTypeValidation(search.ProductType);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.DefaultBuyPrice.ToString()))
                            search.DefaultBuyPrice = -999;
                        //codeMessage = ProductTypeValidation(search.ProductType);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.DefaultSellPrice.ToString()))
                            search.DefaultSellPrice = -999;
                        //codeMessage = ProductTypeValidation(search.ProductType);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.MonthsInAdvance.ToString()))
                            search.MonthsInAdvance = -999;
                        //codeMessage = ProductTypeValidation(search.ProductType);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }
            object returnObj = UpdateProduct_cSec2(search);
            try
            {
                IEnumerable<Product> returnObjValid = (IEnumerable<Product>)returnObj;
                if (returnObjValid.Count() == 0 && emptyErrors)
                {
                    GetProducts newSearch = new GetProducts();
                    newSearch.ProductCode = search.ProductCode;
                    returnObj = (IEnumerable<Product>)GetProducts_cSec2(newSearch);
                    if (returnObjValid.Count() == 0 && emptyErrors)
                        return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
                    returnObj = returnObjValid;
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, (IEnumerable<APIResults_Error_Output>)returnObj);
            }
            /*IEnumerable<Product> returnObj = (IEnumerable<Product>)UpdateProduct_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
            {
                //return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
                GetProducts newSearch = new GetProducts();
                newSearch.ProductCode = search.ProductCode;
                returnObj = (IEnumerable<Product>)GetProducts_cSec2(newSearch);
                if (returnObj.Count() == 0 && emptyErrors)
                    return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            }*/
            return Ok(returnObj);
        }
        private object UpdateProduct_cSec2(UpdateProduct search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_UpdateProduct_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("ProductCode", SqlDbType.VarChar).Value = search.ProductCode;
                    com.Parameters.Add("ProductGroup", SqlDbType.VarChar).Value = search.ProductGroup;
                    com.Parameters.Add("Description", SqlDbType.VarChar).Value = search.Description;
                    com.Parameters.Add("Period", SqlDbType.VarChar).Value = search.Period;
                    com.Parameters.Add("ProductType", SqlDbType.VarChar).Value = search.ProductType;
                    com.Parameters.Add("DefaultSellPrice", SqlDbType.Decimal).Value = search.DefaultSellPrice;
                    com.Parameters.Add("DefaultBuyPrice", SqlDbType.Decimal).Value = search.DefaultBuyPrice;
                    com.Parameters.Add("MonthsInAdvance", SqlDbType.Int).Value = search.MonthsInAdvance;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    //List<Product> ret = dt.ToListCollection<Product>();
                    //return ret.AsEnumerable<Product>();
                    if (dt.Columns[0].ColumnName == "Code" && dt.Columns[1].ColumnName == "Message")
                        return ((dt.ToListCollection<APIResults_Error_Output>()).AsEnumerable<APIResults_Error_Output>());
                    return ((dt.ToListCollection<Product>()).AsEnumerable<Product>());
                }
                catch (Exception e)
                {
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }

        /// <summary>
        /// Get list of prefixes
        /// </summary>
        /// <param name="search"></param>
        /// <returns>DestinationPrefix</returns>
        [Authorize]
        [HttpPost]
        [Route("api/GetDestinationPrefixes")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object GetDestinationPrefixes_Validation(GetDestinationPrefixes search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Area))
                            search.Area = "";
                        //codeMessage = AreaValidation(search.Area);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Country))
                            search.Country = "";
                        //codeMessage = AreaValidation(search.Country);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Prefix))
                            search.Prefix = "";
                        //codeMessage = PrefixValidation(search.Prefix);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Date))
                            search.Date = defaultDate;
                        //codeMessage = DateValidation(search.Date);
                    }                  
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }

            IEnumerable<DestinationPrefix> returnObj = (IEnumerable<DestinationPrefix>)GetDestinationPrefixes_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
                return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            return Ok(returnObj);
        }
        private object GetDestinationPrefixes_cSec2(GetDestinationPrefixes search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_GetDestinationPrefixes_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("Area", SqlDbType.VarChar).Value = search.Area;
                    com.Parameters.Add("Country", SqlDbType.VarChar).Value = search.Country;
                    com.Parameters.Add("Prefix", SqlDbType.VarChar).Value = search.Prefix;
                    com.Parameters.Add("Date", SqlDbType.VarChar).Value = search.Date;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    List<DestinationPrefix> ret = dt.ToListCollection<DestinationPrefix>();
                    return ret.AsEnumerable<DestinationPrefix>();
                }
                catch (Exception e)
                {
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }

        /// <summary>
        /// Create Prefix record
        /// </summary>
        /// <param name="search"></param>
        /// <returns>DestinationPrefix</returns>
        [Authorize]
        [HttpPost]
        [Route("api/CreateDestinationPrefix")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object CreateDestinationPrefix_Validation(CreateDestinationPrefix search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Area))
                            search.Area = "*";
                        codeMessage = AreaValidation(search.Area);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Prefix))
                            search.Prefix = "";
                        //codeMessage = PrefixValidation(search.Prefix);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.EffectiveDate))
                            search.EffectiveDate = defaultDate;
                        codeMessage = DateValidation(search.EffectiveDate);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.ExpiryDate))
                            search.ExpiryDate = defaultExpiryDate;
                        codeMessage = DateValidation(search.ExpiryDate);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }
            object returnObj = CreateDestinationPrefix_cSec2(search);
            try
            {
                IEnumerable<DestinationPrefix> returnObjValid = (IEnumerable<DestinationPrefix>)returnObj;
                if (returnObjValid.Count() == 0 && emptyErrors)
                {
                    GetDestinationPrefixes newSearch = new GetDestinationPrefixes();
                    newSearch.Area = search.Area;
                    newSearch.Country = search.Country;
                    newSearch.Prefix = search.Prefix;
                    newSearch.Date = search.EffectiveDate;
                    returnObj = (IEnumerable<DestinationPrefix>)GetDestinationPrefixes_cSec2(newSearch);
                    if (returnObjValid.Count() == 0 && emptyErrors)
                        return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
                    returnObj = returnObjValid;
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, (IEnumerable<APIResults_Error_Output>)returnObj);
            }
            /*
            IEnumerable<DestinationPrefix> returnObj = (IEnumerable<DestinationPrefix>)CreateDestinationPrefix_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
            {
                //return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
                GetDestinationPrefixes newSearch = new GetDestinationPrefixes();
                newSearch.Area = search.Area;
                newSearch.Prefix = search.Prefix;
                newSearch.Date = search.EffectiveDate;
                returnObj = (IEnumerable<DestinationPrefix>)GetDestinationPrefixes_cSec2(newSearch);
                if (returnObj.Count() == 0 && emptyErrors)
                    return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            }*/
            return Ok(returnObj);
        }
        private object CreateDestinationPrefix_cSec2(CreateDestinationPrefix search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_CreateDestinationPrefix_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("Area", SqlDbType.VarChar).Value = search.Area;
                    com.Parameters.Add("Prefix", SqlDbType.VarChar).Value = search.Prefix;
                    com.Parameters.Add("EffectiveDate", SqlDbType.VarChar).Value = search.EffectiveDate;
                    com.Parameters.Add("ExpiryDate", SqlDbType.VarChar).Value = search.ExpiryDate;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    //List<DestinationPrefix> ret = dt.ToListCollection<DestinationPrefix>();
                    //return ret.AsEnumerable<DestinationPrefix>();
                    if (dt.Columns[0].ColumnName == "Code" && dt.Columns[1].ColumnName == "Message")
                        return ((dt.ToListCollection<APIResults_Error_Output>()).AsEnumerable<APIResults_Error_Output>());
                    return ((dt.ToListCollection<DestinationPrefix>()).AsEnumerable<DestinationPrefix>());
                }
                catch (Exception e)
                {
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }

        /// <summary>
        /// update prefix records
        /// </summary>
        /// <param name="search"></param>
        /// <returns>DestinationPrefix</returns>
        [Authorize]
        [HttpPost]
        [Route("api/UpdateDestinationPrefix")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object UpdateDestinationPrefix_Validation(UpdateDestinationPrefix search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Area))
                            search.Area = defaultWildcardString;
                        //codeMessage = AreaValidation(search.Area);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Prefix))
                            search.Prefix = defaultWildcardString;
                        //codeMessage = PrefixValidation(search.Prefix);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.EffectiveDate))
                            search.EffectiveDate = defaultWildcardString;
                        //codeMessage = DateValidation(search.EffectiveDate);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.ExpiryDate))
                            search.ExpiryDate = defaultWildcardString;
                        //codeMessage = DateValidation(search.ExpiryDate);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }
            object returnObj = UpdateDestinationPrefix_cSec2(search);
            try
            {
                IEnumerable<DestinationPrefix> returnObjValid = (IEnumerable<DestinationPrefix>)returnObj;
                if (returnObjValid.Count() == 0 && emptyErrors)
                {
                    GetDestinationPrefixes newSearch = new GetDestinationPrefixes();
                    newSearch.Area = search.Area;
                    newSearch.Prefix = search.Prefix;
                    newSearch.Date = search.EffectiveDate;
                    returnObj = (IEnumerable<DestinationPrefix>)GetDestinationPrefixes_cSec2(newSearch);
                    if (returnObjValid.Count() == 0 && emptyErrors)
                        return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
                    returnObj = returnObjValid;
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, (IEnumerable<APIResults_Error_Output>)returnObj);
            }
            /*IEnumerable<DestinationPrefix> returnObj = (IEnumerable<DestinationPrefix>)UpdateDestinationPrefix_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
            {
                //return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
                GetDestinationPrefixes newSearch = new GetDestinationPrefixes();
                newSearch.Area = search.Area;
                newSearch.Prefix = search.Prefix;
                newSearch.Date = search.EffectiveDate;
                returnObj = (IEnumerable<DestinationPrefix>)GetDestinationPrefixes_cSec2(newSearch);
                if (returnObj.Count() == 0 && emptyErrors)
                    return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            }*/
            return Ok(returnObj);
        }
        private object UpdateDestinationPrefix_cSec2(UpdateDestinationPrefix search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_UpdateDestinationPrefix_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("Area", SqlDbType.VarChar).Value = search.Area;
                    com.Parameters.Add("Prefix", SqlDbType.VarChar).Value = search.Prefix;
                    com.Parameters.Add("EffectiveDate", SqlDbType.VarChar).Value = search.EffectiveDate;
                    com.Parameters.Add("ExpiryDate", SqlDbType.VarChar).Value = search.ExpiryDate;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    //List<DestinationPrefix> ret = dt.ToListCollection<DestinationPrefix>();
                    //return ret.AsEnumerable<DestinationPrefix>();
                    if (dt.Columns[0].ColumnName == "Code" && dt.Columns[1].ColumnName == "Message")
                        return ((dt.ToListCollection<APIResults_Error_Output>()).AsEnumerable<APIResults_Error_Output>());
                    return ((dt.ToListCollection<DestinationPrefix>()).AsEnumerable<DestinationPrefix>());
                }
                catch (Exception e)
                {
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }

        /// <summary>
        /// Get Areas
        /// </summary>
        /// <param name="search"></param>
        /// <returns>Destination</returns>
        [Authorize]
        [HttpPost]
        [Route("api/GetDestinations")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object GetDestinations_Validation(GetDestinations search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Area))
                            search.Area = "";
                        //codeMessage = AreaValidation(search.Area);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Country))
                            search.Country = "";
                        //codeMessage = CountryValidation(search.Country);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Description))
                            search.Description = "";
                        //codeMessage = DescriptionValidation(search.Description);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Mobile.ToString()))
                            search.Mobile = -1;
                        //codeMessage = MobileValidation(search.Mobile);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }

            IEnumerable<Destination> returnObj = (IEnumerable<Destination>)GetDestinations_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
                return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            return Ok(returnObj);
        }
        private object GetDestinations_cSec2(GetDestinations search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_GetDestinations_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("Area", SqlDbType.VarChar).Value = search.Area;
                    com.Parameters.Add("Country", SqlDbType.VarChar).Value = search.Country;
                    com.Parameters.Add("Description", SqlDbType.VarChar).Value = search.Description; 
                    com.Parameters.Add("Mobile", SqlDbType.Int).Value = search.Mobile;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    List<Destination> ret = dt.ToListCollection<Destination>();
                    return ret.AsEnumerable<Destination>();
                }
                catch (Exception e)
                {
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }

        /// <summary>
        /// Create area
        /// </summary>
        /// <param name="search"></param>
        /// <returns>Destination</returns>
        [Authorize]
        [HttpPost]
        [Route("api/CreateDestination")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object CreateDestination_Validation(CreateDestination search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Area))
                            search.Area = "";
                        //codeMessage = AreaValidation(search.Area);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Country))
                            search.Country = "";
                        //codeMessage = CountryValidation(search.Country);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Description))
                            search.Description = "";
                        //codeMessage = DescriptionValidation(search.Description);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }
            object returnObj = CreateDestination_cSec2(search);
            try
            {
                IEnumerable<Destination> returnObjValid = (IEnumerable<Destination>)returnObj;
                if (returnObjValid.Count() == 0 && emptyErrors)
                {
                    GetDestinations newSearch = new GetDestinations();
                    newSearch.Area = search.Area;
                    newSearch.Country = search.Country;
                    newSearch.Description = search.Description;
                    newSearch.Mobile = search.Mobile;
                    returnObj = (IEnumerable<Destination>)GetDestinations_cSec2(newSearch);
                    if (returnObjValid.Count() == 0 && emptyErrors)
                        return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
                    returnObj = returnObjValid;
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, (IEnumerable<APIResults_Error_Output>)returnObj);
            }
            /*IEnumerable<Destination> returnObj = (IEnumerable<Destination>)CreateDestination_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
            {
                //return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
                GetDestinations newSearch = new GetDestinations();
                newSearch.Area = search.Area;
                newSearch.Country = search.Country;
                newSearch.Description = search.Description;
                newSearch.Mobile = search.Mobile;
                returnObj = (IEnumerable<Destination>)GetDestinations_cSec2(newSearch);
                if (returnObj.Count() == 0 && emptyErrors)
                    return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            }*/
            return Ok(returnObj);
        }
        private object CreateDestination_cSec2(CreateDestination search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_CreateDestination_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("Area", SqlDbType.VarChar).Value = search.Area;
                    com.Parameters.Add("Country", SqlDbType.VarChar).Value = search.Country;
                    com.Parameters.Add("Description", SqlDbType.VarChar).Value = search.Description;
                    com.Parameters.Add("Mobile", SqlDbType.Int).Value = search.Mobile;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    //List<Destination> ret = dt.ToListCollection<Destination>();
                    //return ret.AsEnumerable<Destination>();
                    if (dt.Columns[0].ColumnName == "Code" && dt.Columns[1].ColumnName == "Message")
                        return ((dt.ToListCollection<APIResults_Error_Output>()).AsEnumerable<APIResults_Error_Output>());
                    return ((dt.ToListCollection<Destination>()).AsEnumerable<Destination>());
                }
                catch (Exception e)
                {
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }
        /// <summary>
        /// Update Area
        /// </summary>
        /// <param name="search"></param>
        /// <returns>Destination</returns>
        [Authorize]
        [HttpPost]
        [Route("api/UpdateDestination")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object UpdateDestination_Validation(UpdateDestination search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Area))
                            search.Area = defaultWildcardString;
                        //codeMessage = AreaValidation(search.Area);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Country))
                            search.Country = defaultWildcardString;
                        //codeMessage = CountryValidation(search.Country);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Description))
                            search.Description = defaultWildcardString;
                        //codeMessage = DescriptionValidation(search.Description);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Mobile.ToString()))
                            search.Mobile = -1;
                        //codeMessage = MobileWValidation(search.Mobile);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }
            object returnObj = UpdateDestination_cSec2(search);
            try
            {
                IEnumerable<Destination> returnObjValid = (IEnumerable<Destination>)returnObj;
                if (returnObjValid.Count() == 0 && emptyErrors)
                {
                    GetDestinations newSearch = new GetDestinations();
                    newSearch.Area = search.Area;
                    newSearch.Country = search.Country;
                    newSearch.Description = search.Description;
                    newSearch.Mobile = search.Mobile;
                    returnObj = (IEnumerable<Destination>)GetDestinations_cSec2(newSearch);
                    if (returnObjValid.Count() == 0 && emptyErrors)
                        return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
                    returnObj = returnObjValid;
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, (IEnumerable<APIResults_Error_Output>)returnObj);
            }
            /*IEnumerable<Destination> returnObj = (IEnumerable<Destination>)UpdateDestination_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
            {
                //return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
                GetDestinations newSearch = new GetDestinations();
                newSearch.Area = search.Area;
                newSearch.Country = search.Country;
                newSearch.Description = search.Description;
                newSearch.Mobile = search.Mobile;
                returnObj = (IEnumerable<Destination>)GetDestinations_cSec2(newSearch);
                if (returnObj.Count() == 0 && emptyErrors)
                    return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            }*/
            return Ok(returnObj);
        }
        private object UpdateDestination_cSec2(UpdateDestination search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_UpdateDestination_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("Area", SqlDbType.VarChar).Value = search.Area;
                    com.Parameters.Add("Country", SqlDbType.VarChar).Value = search.Country;
                    com.Parameters.Add("Description", SqlDbType.VarChar).Value = search.Description;
                    com.Parameters.Add("Mobile", SqlDbType.Int).Value = search.Mobile;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    //List<Destination> ret = dt.ToListCollection<Destination>();
                    //return ret.AsEnumerable<Destination>();
                    if (dt.Columns[0].ColumnName == "Code" && dt.Columns[1].ColumnName == "Message")
                        return ((dt.ToListCollection<APIResults_Error_Output>()).AsEnumerable<APIResults_Error_Output>());
                    return ((dt.ToListCollection<Destination>()).AsEnumerable<Destination>());
                }
                catch (Exception e)
                {
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }


        [Authorize]
        [HttpPost]
        [Route("api/GetCustomerProducts")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object GetCustomerProducts_Validation(GetCustomerProducts search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.AccountNumber))
                            search.AccountNumber = "";
                        //codeMessage = AccountNumberValidation(search.Customer);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Telephone))
                            search.Telephone = "";
                        //codeMessage = TelephoneValidation(search.Telephone);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Product))
                            search.Product = "";
                        //codeMessage = ProductValidation(search.Product);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Date))
                            search.Date = "";
                        //codeMessage = DateValidation(search.Date);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.CPID))
                            search.CPID = "";
                        //codeMessage = CPIDValidation(search.CPID);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }
            
            IEnumerable<CustomerProduct> returnObj = (IEnumerable<CustomerProduct>)GetCustomerProducts_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
                return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            return Ok(returnObj);
            
        }
        private object GetCustomerProducts_cSec2(GetCustomerProducts search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_GetCustomerProducts_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("AccountNumber", SqlDbType.VarChar).Value = search.AccountNumber;
                    com.Parameters.Add("Telephone", SqlDbType.VarChar).Value = search.Telephone;
                    com.Parameters.Add("Product", SqlDbType.VarChar).Value = search.Product;
                    com.Parameters.Add("Date", SqlDbType.VarChar).Value = search.Date;
                    com.Parameters.Add("CPID", SqlDbType.VarChar).Value = search.CPID;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    List<CustomerProduct> ret = dt.ToListCollection<CustomerProduct>();
                    return ret.AsEnumerable<CustomerProduct>();
                }
                catch (Exception e)
                {
                     return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/CreateCustomerProduct")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object CreateCustomerProduct_Validation(CreateCustomerProduct search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.AccountNumber))
                            search.AccountNumber = "";
                        //codeMessage = AccountNumberValidation(search.Customer);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Telephone))
                            search.Telephone = "";
                        //codeMessage = TelephoneValidation(search.Telephone);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.ProductCode))
                            search.ProductCode = "";
                        //codeMessage = ProductValidation(search.Product);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.DescriptionOverride))
                            search.DescriptionOverride = "";
                        //codeMessage = DescriptionValidation(search.Description);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.EffectiveDate))
                            search.EffectiveDate = defaultDate;
                        //codeMessage = DateValidation(search.EffectiveDate);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.NextBillStartDate))
                            search.NextBillStartDate = defaultDate;
                        //codeMessage = DateValidation(search.NextBillStartDate);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.ExpiryDate))
                            search.ExpiryDate = defaultExpiryDate;
                        //codeMessage = DateValidation(search.ExpiryDate);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Units.ToString()))
                            search.Units = 0;
                        //codeMessage = UnitValidation(search.Units);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.ApplyRateOverride.ToString()))
                            search.ApplyRateOverride = 0;
                        //codeMessage = RateOverrideValidation(search.ApplyRateOverride);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.RateOverride.ToString()))
                            search.RateOverride = 0;
                        //codeMessage = RateOverrideAmountValidation(search.RateOverrideAmount);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.ShowOnInvoice.ToString()))
                            search.ShowOnInvoice = 0;
                        //codeMessage = ShowOnInvoiceValidation(search.ShowOnInvoice);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.CostOverride.ToString()))
                            search.CostOverride = -999;
                        //codeMessage = CostOverrideValidation(search.CostOverride);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.CPID))
                            search.CPID = "";
                        //codeMessage = CPIDValidation(search.CPID);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Currency))
                            search.Currency = "";
                        //codeMessage = CurrencyValidation(search.Currency);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }
            object returnObj = CreateCustomerProduct_cSec2(search);
            try
            {
                IEnumerable<CustomerProduct> returnObjValid = (IEnumerable<CustomerProduct>)returnObj;
                if (returnObjValid.Count() == 0 && emptyErrors)
                {
                    //return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
                    GetCustomerProducts newSearch = new GetCustomerProducts();
                    newSearch.AccountNumber = search.AccountNumber;
                    newSearch.Telephone = search.Telephone;
                    newSearch.Product = search.ProductCode;
                    newSearch.Date = search.EffectiveDate;
                    newSearch.CPID = search.CPID;
                    returnObjValid = (IEnumerable<CustomerProduct>)GetCustomerProducts_cSec2(newSearch);
                    if (returnObjValid.Count() == 0 && emptyErrors)
                        return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
                    returnObj = returnObjValid;
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, (IEnumerable<APIResults_Error_Output>)returnObj);
            }
            /*
            IEnumerable<CustomerProduct> returnObj = (IEnumerable<CustomerProduct>)CreateCustomerProduct_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
            {
                //return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
                GetCustomerProducts newSearch = new GetCustomerProducts();
                newSearch.Customer = search.Customer;
                newSearch.Telephone = search.Telephone;
                newSearch.Product = search.ProductCode;
                newSearch.Date = search.EffectiveDate;
                newSearch.CPID = search.CPID;
                returnObj = (IEnumerable<CustomerProduct>)GetCustomerProducts_cSec2(newSearch);
                if (returnObj.Count() == 0 && emptyErrors)
                    return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            }
            */
            return Ok(returnObj);
        }
        private object CreateCustomerProduct_cSec2(CreateCustomerProduct search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_CreateCustomerProduct_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("AccountNumber", SqlDbType.VarChar).Value = search.AccountNumber;
                    com.Parameters.Add("Telephone", SqlDbType.VarChar).Value = search.Telephone;
                    com.Parameters.Add("Product", SqlDbType.VarChar).Value = search.ProductCode;
                    com.Parameters.Add("Units", SqlDbType.Int).Value = search.Units;
                    com.Parameters.Add("EffectiveDate", SqlDbType.VarChar).Value = search.EffectiveDate;
                    com.Parameters.Add("NextBillStartDate", SqlDbType.VarChar).Value = search.NextBillStartDate;
                    com.Parameters.Add("ExpiryDate", SqlDbType.VarChar).Value = search.ExpiryDate;
                    com.Parameters.Add("Description", SqlDbType.VarChar).Value = search.DescriptionOverride;
                    com.Parameters.Add("Currency", SqlDbType.VarChar).Value = search.Currency;
                    com.Parameters.Add("ShowOnInvoice", SqlDbType.Int).Value = search.ShowOnInvoice;
                    com.Parameters.Add("RateOverride", SqlDbType.Int).Value = search.ApplyRateOverride;
                    com.Parameters.Add("RateOverrideAmount", SqlDbType.Decimal).Value = search.RateOverride;
                    com.Parameters.Add("CostOverride", SqlDbType.Decimal).Value = search.CostOverride;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    //List<CustomerProduct> ret = dt.ToListCollection<CustomerProduct>();
                    //return ret.AsEnumerable<CustomerProduct>();
                    if(dt.Columns[0].ColumnName=="Code" && dt.Columns[1].ColumnName == "Message")
                        return ((dt.ToListCollection<APIResults_Error_Output>()).AsEnumerable<APIResults_Error_Output>());
                    return ((dt.ToListCollection<CustomerProduct>()).AsEnumerable<CustomerProduct>());

                    /*
                    List<CustomerProduct> ret = dt.ToListCollection<CustomerProduct>();

                    if (ret.Count > 0)
                    {
                        CustomerProduct resCheck = ret[0];
                        if (String.IsNullOrEmpty(resCheck.CPID))
                        {
                            List<APIResults_Error_Output> retCM = dt.ToListCollection<APIResults_Error_Output>();
                            return retCM.AsEnumerable<APIResults_Error_Output>();
                        }
                        return ret.AsEnumerable<CustomerProduct>();
                    }
                    List<APIResults_Error_Output> ret2 = dt.ToListCollection<APIResults_Error_Output>();
                    return ret2.AsEnumerable<APIResults_Error_Output>();
                }
                    catch (Exception e)
                    {
                        List<APIResults_Error_Output> ret = dt.ToListCollection<APIResults_Error_Output>();
                        return ret.AsEnumerable<APIResults_Error_Output>();
                    }*/
                }
                catch (Exception e)
                {
                    return genErrorResult(generalErrorCode, generalErrorMessage+": "+e.Message);
                }
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/UpdateCustomerProduct")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object UpdateCustomerProduct_Validation(UpdateCustomerProduct search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.AccountNumber))
                            search.AccountNumber = defaultWildcardString;
                        //codeMessage = AccountNumberValidation(search.Customer);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Telephone))
                            search.Telephone = defaultWildcardString;
                        //codeMessage = TelephoneValidation(search.Telephone);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.ProductCode))
                            search.ProductCode = defaultWildcardString;
                        //codeMessage = ProductValidation(search.Product);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.DescriptionOverride))
                            search.DescriptionOverride = defaultWildcardString;
                        //codeMessage = DescriptionValidation(search.Description);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.EffectiveDate))
                            search.EffectiveDate = defaultWildcardString;
                        //codeMessage = DateValidation(search.EffectiveDate);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.NextBillStartDate))
                            search.NextBillStartDate = defaultWildcardString;
                        //codeMessage = DateValidation(search.NextBillStartDate);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.ExpiryDate))
                            search.ExpiryDate = defaultWildcardString;
                        //codeMessage = DateValidation(search.ExpiryDate);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Units.ToString()))
                            search.Units = -999;
                        //codeMessage = UnitValidation(search.Units);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.ApplyRateOverride.ToString()))
                            search.ApplyRateOverride = -999;
                        //codeMessage = RateOverrideValidation(search.ApplyRateOverride);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.RateOverride.ToString()))
                            search.RateOverride = -999;
                        //codeMessage = RateOverrideAmountValidation(search.RateOverrideAmount);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.ShowOnInvoice.ToString()))
                            search.ShowOnInvoice = -999;
                        //codeMessage = ShowOnInvoiceValidation(search.ShowOnInvoice);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.CostOverride.ToString()))
                            search.CostOverride = -999;
                        //codeMessage = CostOverrideValidation(search.CostOverride);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.CPID))
                            search.CPID = "";
                        //codeMessage = CPIDValidation(search.CPID);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Currency))
                            search.Currency = defaultWildcardString;
                        //codeMessage = CurrencyValidation(search.Currency);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }
            object returnObj = UpdateCustomerProduct_cSec2(search);
            try
            {
                IEnumerable<CustomerProduct> returnObjValid = (IEnumerable<CustomerProduct>)returnObj;
                if (returnObjValid.Count() == 0 && emptyErrors)
                {
                    //return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
                    GetCustomerProducts newSearch = new GetCustomerProducts();
                    newSearch.AccountNumber = search.AccountNumber;
                    newSearch.Telephone = search.Telephone;
                    newSearch.Product = search.ProductCode;
                    newSearch.Date = search.EffectiveDate;
                    newSearch.CPID = search.CPID;
                    returnObjValid = (IEnumerable<CustomerProduct>)GetCustomerProducts_cSec2(newSearch);
                    if (returnObjValid.Count() == 0 && emptyErrors)
                        return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
                    returnObj=returnObjValid;
                }
            }
            catch(Exception e)
            {    
                //return Ok();
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, (IEnumerable<APIResults_Error_Output>)returnObj);
            }
            //IEnumerable<CustomerProduct> returnObj = (IEnumerable<CustomerProduct>)UpdateCustomerProduct_cSec2(search);
            return Ok(returnObj);
        }
        private object UpdateCustomerProduct_cSec2(UpdateCustomerProduct search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_UpdateCustomerProduct_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("AccountNumber", SqlDbType.VarChar).Value = search.AccountNumber;
                    com.Parameters.Add("Telephone", SqlDbType.VarChar).Value = search.Telephone;
                    com.Parameters.Add("Product", SqlDbType.VarChar).Value = search.ProductCode;
                    com.Parameters.Add("Units", SqlDbType.Int).Value = search.Units;
                    com.Parameters.Add("EffectiveDate", SqlDbType.VarChar).Value = search.EffectiveDate;
                    com.Parameters.Add("NextBillStartDate", SqlDbType.VarChar).Value = search.NextBillStartDate;
                    com.Parameters.Add("ExpiryDate", SqlDbType.VarChar).Value = search.ExpiryDate;
                    com.Parameters.Add("Description", SqlDbType.VarChar).Value = search.DescriptionOverride;
                    com.Parameters.Add("Currency", SqlDbType.VarChar).Value = search.Currency;
                    com.Parameters.Add("ShowOnInvoice", SqlDbType.Int).Value = search.ShowOnInvoice;
                    com.Parameters.Add("RateOverride", SqlDbType.Int).Value = search.ApplyRateOverride;
                    com.Parameters.Add("RateOverrideAmount", SqlDbType.Decimal).Value = search.RateOverride;
                    com.Parameters.Add("CostOverride", SqlDbType.Decimal).Value = search.CostOverride;
                    com.Parameters.Add("CPID", SqlDbType.VarChar).Value = search.CPID;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    if (dt.Columns[0].ColumnName == "Code" && dt.Columns[1].ColumnName == "Message")
                        return ((dt.ToListCollection<APIResults_Error_Output>()).AsEnumerable<APIResults_Error_Output>());
                    return ((dt.ToListCollection<CustomerProduct>()).AsEnumerable<CustomerProduct>());
                    /*
                    try
                    {
                        List<CustomerProduct> ret = dt.ToListCollection<CustomerProduct>();
                        CustomerProduct resCheck = ret[0];
                        if (String.IsNullOrEmpty(resCheck.CPID))
                        {
                            List<APIResults_Error_Output> retCM = dt.ToListCollection<APIResults_Error_Output>();
                            return retCM.AsEnumerable<APIResults_Error_Output>();
                        }
                        return ret.AsEnumerable<CustomerProduct>();

                    }
                    catch(Exception e)
                    {
                        List<APIResults_Error_Output> ret = dt.ToListCollection<APIResults_Error_Output>();
                        return ret.AsEnumerable<APIResults_Error_Output>();
                    }  */                  
                }
                catch (Exception e)
                {
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }

        /// <summary>
        /// Get Invoices for entered parameters
        /// </summary>
        /// <param name="search"></param>
        /// <returns>Invoice</returns>
        [Authorize]
        [HttpPost]
        [Route("api/GetInvoices")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object GetInvoices_Validation(GetInvoices search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.AccountNumber))
                            search.AccountNumber = "";
                        //codeMessage = AccountNumberValidation(search.AccountNumber);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Date))
                            search.Date = "";
                        //codeMessage = DateValidation(search.Date);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }

            IEnumerable<Invoice> returnObj = (IEnumerable<Invoice>)GetInvoices_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
                return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            return Ok(returnObj);

        }
        private object GetInvoices_cSec2(GetInvoices search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_GetInvoices_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("AccountNumber", SqlDbType.VarChar).Value = search.AccountNumber;
                    com.Parameters.Add("Date", SqlDbType.VarChar).Value = search.Date;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    List<Invoice> ret = dt.ToListCollection<Invoice>();
                    return ret.AsEnumerable<Invoice>();
                }
                catch (Exception e)
                {
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }

        /// <summary>
        /// Get Lines for specified invoice
        /// </summary>
        /// <param name="search"></param>
        /// <returns>InvoiceLine</returns>
        [Authorize]
        [HttpPost]
        [Route("api/GetInvoiceLines")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object GetInvoiceLines_Validation(GetInvoiceLines search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.InvoiceNumber))
                            search.InvoiceNumber = "";
                        //codeMessage = InvoiceNumberValidation(search.InvoiceNumber);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }

            IEnumerable<InvoiceLine> returnObj = (IEnumerable<InvoiceLine>)GetInvoiceLines_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
                return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            return Ok(returnObj);

        }
        private object GetInvoiceLines_cSec2(GetInvoiceLines search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_GetInvoiceLines_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("InvoiceNumber", SqlDbType.VarChar).Value = search.InvoiceNumber;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    List<InvoiceLine> ret = dt.ToListCollection<InvoiceLine>();
                    return ret.AsEnumerable<InvoiceLine>();
                }
                catch (Exception e)
                {
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }
        /// <summary>
        /// Get Lines for specified invoice
        /// </summary>
        /// <param name="search"></param>
        /// <returns>InvoiceLine</returns>
        [Authorize]
        [HttpPost]
        [Route("api/GetSummarisedCalls")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object GetSummarisedCalls_Validation(GetSummarisedCalls search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.InvoiceNumber))
                            search.InvoiceNumber = "";
                        //codeMessage = InvoiceNumberValidation(search.InvoiceNumber);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }

            IEnumerable<SummarisedCalls> returnObj = (IEnumerable<SummarisedCalls>)GetSummarisedCalls_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
                return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            return Ok(returnObj);

        }
        private object GetSummarisedCalls_cSec2(GetSummarisedCalls search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_GetSummarisedCalls_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("InvoiceNumber", SqlDbType.VarChar).Value = search.InvoiceNumber;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    List<SummarisedCalls> ret = dt.ToListCollection<SummarisedCalls>();
                    return ret.AsEnumerable<SummarisedCalls>();
                }
                catch (Exception e)
                {
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }
        /// <summary>
        /// Get Unbilled CDRs
        /// </summary>
        /// <param name="search"></param>
        /// <returns>CDR</returns>
        [Authorize]
        [HttpPost]
        [Route("api/GetUnbilledCDRs")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object GetUnbilledCDRs_Validation(GetUnbilledCDRs search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.AccountNumber))
                            search.AccountNumber = "";
                        //codeMessage = AccountNumberValidation(search.AccountNumber);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.StartDate))
                            search.StartDate = defaultDate;
                        //codeMessage = DateValidation(search.Date);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.EndDate))
                            search.EndDate = defaultExpiryDate;
                        //codeMessage = DateValidation(search.Date);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }

            IEnumerable<CDR> returnObj = (IEnumerable<CDR>)GetUnbilledCDRs_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
                return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            return Ok(returnObj);

        }
        private object GetUnbilledCDRs_cSec2(GetUnbilledCDRs search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_GetUnbilledCDRs_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("AccountNumber", SqlDbType.VarChar).Value = search.AccountNumber;
                    com.Parameters.Add("StartDate", SqlDbType.VarChar).Value = search.StartDate;
                    com.Parameters.Add("EndDate", SqlDbType.VarChar).Value = search.EndDate;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    List<CDR> ret = dt.ToListCollection<CDR>();
                    return ret.AsEnumerable<CDR>();
                }
                catch (Exception e)
                {
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }

        /// <summary>
        /// Get Billed CDRs
        /// </summary>
        /// <param name="search"></param>
        /// <returns>CDR</returns>
        [Authorize]
        [HttpPost]
        [Route("api/GetBilledCDRs")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object GetBilledCDRs_Validation(GetBilledCDRs search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.AccountNumber))
                            search.AccountNumber = "";
                        //codeMessage = AccountNumberValidation(search.AccountNumber);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.InvoiceNumber))
                            search.InvoiceNumber = "";
                        //codeMessage = InvoiceNumberValidation(search.InvoiceNumber);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }

            IEnumerable<CDR> returnObj = (IEnumerable<CDR>)GetBilledCDRs_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
                return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            return Ok(returnObj);

        }
        private object GetBilledCDRs_cSec2(GetBilledCDRs search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_GetBilledCDRs_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("AccountNumber", SqlDbType.VarChar).Value = search.AccountNumber;
                    com.Parameters.Add("InvoiceNumber", SqlDbType.VarChar).Value = search.InvoiceNumber;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    List<CDR> ret = dt.ToListCollection<CDR>();
                    return ret.AsEnumerable<CDR>();
                }
                catch (Exception e)
                {
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }

        /// <summary>
        /// Get Filtered CDRs
        /// </summary>
        /// <param name="search"></param>
        /// <returns>CDR</returns>
        [Authorize]
        [HttpPost]
        [Route("api/GetFilteredCDRs")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object GetBilledCDRs_Validation(GetFilteredCDRs search)
        {
            //var t= ControllerContext.RequestContext.Principal.Identity.Name;
            //var t2 = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
           // var t2 = ((System.Web.HttpRequestWrapper)((System.Web.Http.WebHost.WebHostHttpRequestContext)ControllerContext.RequestContext).WebRequest).UserHostAddress;
            //var wt = ControllerContext.Request.RemoteIpAddress;
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.AccountNumber))
                            search.AccountNumber = "";
                        //codeMessage = AccountNumberValidation(search.AccountNumber);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.InvoiceNumber))
                            search.InvoiceNumber = "";
                        //codeMessage = InvoiceNumberValidation(search.InvoiceNumber);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.ANumber))
                            search.ANumber = "";
                        //codeMessage = InvoiceNumberValidation(search.InvoiceNumber);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.BNumber))
                            search.BNumber = "";
                        //codeMessage = InvoiceNumberValidation(search.InvoiceNumber);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.CNumber))
                            search.CNumber = "";
                        //codeMessage = InvoiceNumberValidation(search.InvoiceNumber);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.DestinationArea))
                            search.DestinationArea = "";
                        //codeMessage = InvoiceNumberValidation(search.InvoiceNumber);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.DestinationCountry))
                            search.DestinationCountry = "";
                        //codeMessage = InvoiceNumberValidation(search.InvoiceNumber);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.ServiceClass))
                            search.ServiceClass = "";
                        //codeMessage = InvoiceNumberValidation(search.InvoiceNumber);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.CallType))
                            search.CallType = "";
                        //codeMessage = InvoiceNumberValidation(search.InvoiceNumber);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.StartDate))
                            search.StartDate = defaultDate;
                        //codeMessage = InvoiceNumberValidation(search.InvoiceNumber);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.EndDate))
                            search.EndDate = defaultExpiryDate;
                        //codeMessage = InvoiceNumberValidation(search.InvoiceNumber);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.FileName))
                            search.FileName = "";
                        //codeMessage = InvoiceNumberValidation(search.InvoiceNumber);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.MinValue.ToString()))
                            search.MinValue = -999;
                        //codeMessage = InvoiceNumberValidation(search.InvoiceNumber);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.MaxValue.ToString()))
                            search.MaxValue=9999;
                        //codeMessage = InvoiceNumberValidation(search.InvoiceNumber);
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }

            IEnumerable<CDR> returnObj = (IEnumerable<CDR>)GetFilteredCDRs_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
                return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            return Ok(returnObj);

        }
        private object GetFilteredCDRs_cSec2(GetFilteredCDRs search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_GetFilteredCDRs_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("AccountNumber", SqlDbType.VarChar).Value = search.AccountNumber;
                    com.Parameters.Add("InvoiceNumber", SqlDbType.VarChar).Value = search.InvoiceNumber;
                    com.Parameters.Add("ANumber", SqlDbType.VarChar).Value = search.ANumber;
                    com.Parameters.Add("BNumber", SqlDbType.VarChar).Value = search.BNumber;
                    com.Parameters.Add("CNumber", SqlDbType.VarChar).Value = search.CNumber;
                    com.Parameters.Add("DestinationArea", SqlDbType.VarChar).Value = search.DestinationArea;
                    com.Parameters.Add("DestinationCountry", SqlDbType.VarChar).Value = search.DestinationCountry;
                    com.Parameters.Add("ServiceClass", SqlDbType.VarChar).Value = search.ServiceClass;
                    com.Parameters.Add("CallType", SqlDbType.VarChar).Value = search.CallType;
                    com.Parameters.Add("StartDate", SqlDbType.VarChar).Value = search.StartDate;
                    com.Parameters.Add("EndDate", SqlDbType.VarChar).Value = search.EndDate;
                    com.Parameters.Add("FileName", SqlDbType.VarChar).Value = search.FileName;
                    com.Parameters.Add("MinValue", SqlDbType.Decimal).Value = search.MinValue;
                    com.Parameters.Add("MaxValue", SqlDbType.Decimal).Value = search.MaxValue;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    List<CDR> ret = dt.ToListCollection<CDR>();
                    return ret.AsEnumerable<CDR>();
                }
                catch (Exception e)
                {
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }

        /// <summary>
        /// Get Invalid CDRs
        /// </summary>
        /// <param name="search"></param>
        /// <returns>InvalidCDR</returns>
        [Authorize]
        [HttpPost]
        [Route("api/GetInvalidCDRs")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object GetInvalidCDRs_Validation(GetInvalidCDRs search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.StartDate))
                            search.StartDate = defaultDate;
                        //codeMessage = DateValidation(search.Date);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.EndDate))
                            search.EndDate = defaultExpiryDate;
                        //codeMessage = DateValidation(search.Date);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }

            IEnumerable<InvalidCDR> returnObj = (IEnumerable<InvalidCDR>)GetInvalidCDRs_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
                return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            return Ok(returnObj);

        }
        private object GetInvalidCDRs_cSec2(GetInvalidCDRs search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_GetInvalidCDRs_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("StartDate", SqlDbType.VarChar).Value = search.StartDate;
                    com.Parameters.Add("EndDate", SqlDbType.VarChar).Value = search.EndDate;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    List<InvalidCDR> ret = dt.ToListCollection<InvalidCDR>();
                    return ret.AsEnumerable<InvalidCDR>();
                }
                catch (Exception e)
                {
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }
 
        [Authorize]
        [HttpPost]
        [Route("api/GetInvoiceGroups")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object GetInvoiceGroups_Validation(GetInvoiceGroups search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.InvoiceGroupID))
                            search.InvoiceGroupID = "";
                        //codeMessage = InvoiceGroupIDValidation(search.InvoiceGroupID);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }

            IEnumerable<InvoiceGroup> returnObj = (IEnumerable<InvoiceGroup>)GetInvoiceGroups_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
                return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            return Ok(returnObj);

        }
        private object GetInvoiceGroups_cSec2(GetInvoiceGroups search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_GetInvoiceGroups_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("InvoiceGroupID", SqlDbType.VarChar).Value = search.InvoiceGroupID;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    List<InvoiceGroup> ret = dt.ToListCollection<InvoiceGroup>();
                    return ret.AsEnumerable<InvoiceGroup>();
                }
                catch (Exception e)
                {
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/RecycleErrors")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object RecycleErrors_Validation(RecycleErrors search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;
            IEnumerable<APIResults_Error_Output> returnObj = (IEnumerable<APIResults_Error_Output>)RecycleErrors_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
                return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            return Ok(returnObj);

        }
        private object RecycleErrors_cSec2(RecycleErrors search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_StartTask_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("Task", SqlDbType.VarChar).Value = "TK_InvalidCDR_Recycle";
                    com.Parameters.Add("Label", SqlDbType.VarChar).Value = "1";
                    com.Parameters.Add("Process", SqlDbType.VarChar).Value = "Recycle CDRs";
                    com.Parameters.Add("SubProcess", SqlDbType.VarChar).Value = "";

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    List<APIResults_Error_Output> ret = dt.ToListCollection<APIResults_Error_Output>();
                    return ret.AsEnumerable<APIResults_Error_Output>();
                }
                catch (Exception e)
                {
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }

        /// <summary>
        /// Rerate CDRs
        /// </summary>
        /// <param name="search"></param>
        /// <returns>InvalidCDR</returns>
        [Authorize]
        [HttpPost]
        [Route("api/RerateCDRs")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object RerateCDRs_Validation(RerateCDRs search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.AccountNumber))
                            search.AccountNumber = "";
                        //codeMessage = AccountNumberValidation(search.AccountNumber);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }

            IEnumerable<APIResults_Error_Output> returnObj = (IEnumerable<APIResults_Error_Output>)RerateCDRs_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
                return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            return Ok(returnObj);

        }
        private object RerateCDRs_cSec2(RerateCDRs search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_StartTask_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("Task", SqlDbType.VarChar).Value = "TK_Rerate";
                    com.Parameters.Add("Label", SqlDbType.VarChar).Value = search.AccountNumber+",01/01/1980,01/01/2036,1";
                    com.Parameters.Add("Process", SqlDbType.VarChar).Value = "Rerate CDRs";
                    com.Parameters.Add("SubProcess", SqlDbType.VarChar).Value = search.AccountNumber;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    List<APIResults_Error_Output> ret = dt.ToListCollection<APIResults_Error_Output>();
                    return ret.AsEnumerable<APIResults_Error_Output>();
                }
                catch (Exception e)
                {
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/RunInvoiceGroup")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object RunInvoiceGroup_Validation(RunInvoiceGroup search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.InvoiceGroupID))
                            search.InvoiceGroupID = "";
                        //codeMessage = InvoiceGroupIDValidation(search.InvoiceGroupID);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }

            IEnumerable<APIResults_Error_Output> returnObj = (IEnumerable<APIResults_Error_Output>)RunInvoiceGroup_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
                return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            return Ok(returnObj);

        }
        private object RunInvoiceGroup_cSec2(RunInvoiceGroup search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_StartTask_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("Task", SqlDbType.VarChar).Value = "TK_RunInvoiceGroup";
                    com.Parameters.Add("Label", SqlDbType.VarChar).Value = search.InvoiceGroupID;
                    com.Parameters.Add("Process", SqlDbType.VarChar).Value = "RunInvoiceGroup";
                    com.Parameters.Add("SubProcess", SqlDbType.VarChar).Value = search.InvoiceGroupID;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    List<APIResults_Error_Output> ret = dt.ToListCollection<APIResults_Error_Output>();
                    return ret.AsEnumerable<APIResults_Error_Output>();
                }
                catch (Exception e)
                {
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }
        [Authorize]
        [HttpPost]
        [Route("api/GetInvoiceStatus")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object GetInvoiceStatus_Validation(GetInvoiceStatus search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Date))
                            search.Date = "";
                        //codeMessage = DateValidation(search.Date);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }

            IEnumerable<InvoiceStatus> returnObj = (IEnumerable<InvoiceStatus>)GetInvoiceStatus_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
                return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            return Ok(returnObj);

        }
        private object GetInvoiceStatus_cSec2(GetInvoiceStatus search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_GetInvoiceStatus_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("Date", SqlDbType.VarChar).Value = search.Date;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    List<InvoiceStatus> ret = dt.ToListCollection<InvoiceStatus>();
                    return ret.AsEnumerable<InvoiceStatus>();
                }
                catch (Exception e)
                {
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }
        [Authorize]
        [HttpPost]
        [Route("api/GetAPILog")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object GetAPILog_Validation(GetAPILog search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.StartDate))
                            search.StartDate = defaultDate;
                        //codeMessage = DateValidation(search.Date);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.EndDate))
                            search.EndDate = defaultExpiryDate;
                        //codeMessage = DateValidation(search.EndDate);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }

            IEnumerable<APILog> returnObj = (IEnumerable<APILog>)GetAPILog_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
                return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            return Ok(returnObj);

        }
        private object GetAPILog_cSec2(GetAPILog search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_GetAPILog_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("StartDate", SqlDbType.VarChar).Value = search.StartDate;
                    com.Parameters.Add("EndDate", SqlDbType.VarChar).Value = search.EndDate;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    List<APILog> ret = dt.ToListCollection<APILog>();
                    return ret.AsEnumerable<APILog>();
                }
                catch (Exception e)
                {
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }
        [Authorize]
        [HttpPost]
        [Route("api/UnbillInvoiceGroup")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object UnbillInvoiceGroup_Validation(RunInvoiceGroup search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.InvoiceGroupID))
                            search.InvoiceGroupID = "";
                        //codeMessage = InvoiceGroupIDValidation(search.InvoiceGroupID);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }

            IEnumerable<APIResults_Error_Output> returnObj = (IEnumerable<APIResults_Error_Output>)UnbillInvoiceGroup_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
                return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            return Ok(returnObj);

        }
        private object UnbillInvoiceGroup_cSec2(RunInvoiceGroup search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_StartTask_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("Task", SqlDbType.VarChar).Value = "TK_Invoice_RetailInvoiceGroup_Unbill_Invoke";
                    com.Parameters.Add("Label", SqlDbType.VarChar).Value = search.InvoiceGroupID;
                    com.Parameters.Add("Process", SqlDbType.VarChar).Value = "UnBillInvoiceGroup";
                    com.Parameters.Add("SubProcess", SqlDbType.VarChar).Value = search.InvoiceGroupID;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    List<APIResults_Error_Output> ret = dt.ToListCollection<APIResults_Error_Output>();
                    return ret.AsEnumerable<APIResults_Error_Output>();
                }
                catch (Exception e)
                {
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/RunSingleInvoice")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object RunInvoice_Validation(RunSingleInvoice search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.AccountNumber))
                            search.InvoiceType = "";
                        //codeMessage = AccountNumberValidation(search.AccountNumber);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.InvoiceEndDate))
                            search.InvoiceEndDate = "";
                        codeMessage = DateValidation(search.InvoiceEndDate);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.InvoiceType))
                            search.InvoiceType = "FI";
                        //codeMessage = InvoiceTypeValidation(search.InvoiceType);
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }

            IEnumerable<APIResults_Error_Output> returnObj = (IEnumerable<APIResults_Error_Output>)RunInvoice_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
                return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            return Ok(returnObj);

        }
        private object RunInvoice_cSec2(RunSingleInvoice search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_StartTask_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("Task", SqlDbType.VarChar).Value = "TK_RunRetailInvoice";
                    com.Parameters.Add("Label", SqlDbType.VarChar).Value = search.AccountNumber+","+search.InvoiceEndDate+",,"+search.InvoiceType+",API";
                    com.Parameters.Add("Process", SqlDbType.VarChar).Value = "RunRetailInvoice";
                    com.Parameters.Add("SubProcess", SqlDbType.VarChar).Value = search.AccountNumber;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    List<APIResults_Error_Output> ret = dt.ToListCollection<APIResults_Error_Output>();
                    return ret.AsEnumerable<APIResults_Error_Output>();
                }
                catch (Exception e)
                {
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/UnbillSingleInvoice")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object UnbillInvoice_Validation(UnbillInvoice search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.InvoiceNumber))
                            search.InvoiceNumber = "";
                        //codeMessage = InvoiceGroupIDValidation(search.InvoiceGroupID);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }

            IEnumerable<APIResults_Error_Output> returnObj = (IEnumerable<APIResults_Error_Output>)UnbillInvoice_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
                return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            return Ok(returnObj);

        }
        private object UnbillInvoice_cSec2(UnbillInvoice search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_StartTask_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("Task", SqlDbType.VarChar).Value = "TK_UnbillRetailInvoice";
                    com.Parameters.Add("Label", SqlDbType.VarChar).Value = search.InvoiceNumber;
                    com.Parameters.Add("Process", SqlDbType.VarChar).Value = "UnbillRetailInvoice";
                    com.Parameters.Add("SubProcess", SqlDbType.VarChar).Value = search.InvoiceNumber;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    List<APIResults_Error_Output> ret = dt.ToListCollection<APIResults_Error_Output>();
                    return ret.AsEnumerable<APIResults_Error_Output>();
                }
                catch (Exception e)
                {
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }


        [Authorize]
        [HttpPost]
        [Route("api/RunReport")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object RunReport_Validation(RunReport search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.ReportID.ToString()))
                            search.ReportID = 0;
                        //codeMessage = ReportIDValidation(search.ReportID);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Param1))
                            search.Param1 = "";
                        //codeMessage = ReportParamValidation(search.Param1);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Param2))
                            search.Param2 = "";
                        //codeMessage = ReportParamValidation(search.Param2);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Param3))
                            search.Param3 = "";
                        //codeMessage = ReportParamValidation(search.Param3);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Param4))
                            search.Param4 = "";
                        //codeMessage = ReportParamValidation(search.Param4);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Param5))
                            search.Param5 = "";
                        //codeMessage = ReportParamValidation(search.Param5);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Param6))
                            search.Param6 = "";
                        //codeMessage = ReportParamValidation(search.Param6);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }

            IEnumerable<APIResults_Error_Output> returnObj = (IEnumerable<APIResults_Error_Output>)RunReport_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
                return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            return Ok(returnObj);

        }
        private object RunReport_cSec2(RunReport search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_RunReport_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("ReportID", SqlDbType.Int).Value = search.ReportID;
                    com.Parameters.Add("Param1", SqlDbType.VarChar).Value = search.Param1;
                    com.Parameters.Add("Param2", SqlDbType.VarChar).Value = search.Param2;
                    com.Parameters.Add("Param3", SqlDbType.VarChar).Value = search.Param3;
                    com.Parameters.Add("Param4", SqlDbType.VarChar).Value = search.Param4;
                    com.Parameters.Add("Param5", SqlDbType.VarChar).Value = search.Param5;
                    com.Parameters.Add("Param6", SqlDbType.VarChar).Value = search.Param6;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    List<APIResults_Error_Output> ret = dt.ToListCollection<APIResults_Error_Output>();
                    return ret.AsEnumerable<APIResults_Error_Output>();
                }
                catch (Exception e)
                {
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }

        /// <summary>
        /// Get List of products bundle details
        /// </summary>
        /// <param name="search"></param>
        /// <returns>Product</returns>
        [Authorize]
        [HttpPost]
        [Route("api/GetProductBundleDetails")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object GetProductBundleDetails_Validation(GetProductBundleDetails search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Product))
                            search.Product = "";
                        //codeMessage = ProductCodeValidation(search.Product);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Area))
                            search.Area = "";
                        //codeMessage = AreaValidation(search.Area);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.CallType))
                            search.CallType = "";
                        //codeMessage = CallTypeValidation(search.CallType);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Prefix))
                            search.Prefix = "";
                        //codeMessage = PrefixValidation(search.Prefix);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.PBDID))
                            search.PBDID = "";
                        //codeMessage = PBDIDValidation(search.PBDID);
                    }                   
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }

            IEnumerable<ProductBundleDetail> returnObj = (IEnumerable<ProductBundleDetail>)GetProductBundleDetails_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
                return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            return Ok(returnObj);
        }
        private object GetProductBundleDetails_cSec2(GetProductBundleDetails search)
        {
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_GetProductBundleDetails_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("Product", SqlDbType.VarChar).Value = search.Product;
                    com.Parameters.Add("Area", SqlDbType.VarChar).Value = search.Area;
                    com.Parameters.Add("CallType", SqlDbType.VarChar).Value = search.CallType;
                    com.Parameters.Add("Prefix", SqlDbType.VarChar).Value = search.Prefix;
                    com.Parameters.Add("PBDID", SqlDbType.VarChar).Value = search.PBDID;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    List<ProductBundleDetail> ret = dt.ToListCollection<ProductBundleDetail>();
                    return ret.AsEnumerable<ProductBundleDetail>();
                }
                catch (Exception e)
                {
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }

        /// <summary>
        /// Create new product bundle detail
        /// </summary>
        /// <param name="search"></param>
        /// <returns>Product</returns>
        [Authorize]
        [HttpPost]
        [Route("api/CreateProductBundleDetail")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object CreateProductBundleDetail_Validation(CreateProductBundleDetail search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.ProductCode))
                            search.ProductCode = "";
                        //codeMessage = ProductCodeValidation(search.ProductCode);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Area))
                            search.Area = "";
                        //codeMessage = AreaValidation(search.Area);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.CallType))
                            search.CallType = "7";
                        //codeMessage = CallTypeValidation(search.CallType);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Prefix))
                            search.Prefix = "";
                        //codeMessage = PrefixValidation(search.Prefix);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.TimeBand))
                            search.TimeBand = "";
                        //codeMessage = TimeBandValidation(search.TimeBand);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.InternalCalls))
                            search.InternalCalls = "0";
                        //codeMessage = InternalCallsValidation(search.InternalCalls);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.PBDID))
                            search.PBDID = "0";
                        //codeMessage = PBDIDValidation(search.PBDID);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }
            object returnObj = CreateProductBundleDetail_cSec2(search);
            try
            {
                IEnumerable<ProductBundleDetail> returnObjValid = (IEnumerable<ProductBundleDetail>)returnObj;
                if (returnObjValid.Count() == 0 && emptyErrors)
                {
                    GetProductBundleDetails newSearch = new GetProductBundleDetails();
                    newSearch.Product = search.ProductCode;
                    newSearch.Area = search.Area;
                    newSearch.CallType = search.CallType;
                    newSearch.Prefix = search.Prefix;
                    newSearch.PBDID = search.PBDID;
                    returnObj = (IEnumerable<ProductBundleDetail>)GetProductBundleDetails_cSec2(newSearch);
                    if (returnObjValid.Count() == 0 && emptyErrors)
                        return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
                    returnObj = returnObjValid;
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, (IEnumerable<APIResults_Error_Output>)returnObj);
            }
            return Ok(returnObj);
        }
        private object CreateProductBundleDetail_cSec2(CreateProductBundleDetail search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_CreateProductBundleDetail_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("ProductCode", SqlDbType.VarChar).Value = search.ProductCode;
                    com.Parameters.Add("Area", SqlDbType.VarChar).Value = search.Area;
                    com.Parameters.Add("CallType", SqlDbType.VarChar).Value = search.CallType;
                    com.Parameters.Add("Prefix", SqlDbType.VarChar).Value = search.Prefix;
                    com.Parameters.Add("TimeBand", SqlDbType.VarChar).Value = search.TimeBand;
                    com.Parameters.Add("InternalCalls", SqlDbType.VarChar).Value = search.InternalCalls;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    //List<Product> ret = dt.ToListCollection<Product>();
                    //return ret.AsEnumerable<Product>();
                    if (dt.Columns[0].ColumnName == "Code" && dt.Columns[1].ColumnName == "Message")
                        return ((dt.ToListCollection<APIResults_Error_Output>()).AsEnumerable<APIResults_Error_Output>());
                    return ((dt.ToListCollection<ProductBundleDetail>()).AsEnumerable<ProductBundleDetail>());
                }
                catch (Exception e)
                {
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }

        /// <summary>
        /// Update product bundle detail record
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("api/UpdateProductBundleDetail")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object UpdateProductBundleDetail_Validation(UpdateProductBundleDetail search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.PBDID))
                            search.PBDID = "REQUIRED";
                        codeMessage = PBDIDValidation(search.PBDID);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Area))
                            search.Area = defaultWildcardString;
                        //codeMessage = AreaValidation(search.Area);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.ProductCode))
                            search.ProductCode = defaultWildcardString;
                        //codeMessage = ProductValidation(search.Product);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.CallType))
                            search.CallType = defaultWildcardString;
                        //codeMessage = CallTypeValidation(search.CallType);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Prefix))
                            search.Prefix = defaultWildcardString;
                        //codeMessage = PrefixValidation(search.Prefix);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.TimeBand))
                            search.TimeBand = defaultWildcardString;
                        //codeMessage = TimeBandValidation(search.TimeBand);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.InternalCalls))
                            search.InternalCalls = defaultWildcardString;
                        //codeMessage = InternalCallsValidation(search.InternalCalls);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }
            object returnObj = UpdateProductBundleDetail_cSec2(search);
            try
            {
                IEnumerable<ProductBundleDetail> returnObjValid = (IEnumerable<ProductBundleDetail>)returnObj;
                if (returnObjValid.Count() == 0 && emptyErrors)
                {
                    GetProductBundleDetails newSearch = new GetProductBundleDetails();
                    newSearch.Product = search.ProductCode;
                    newSearch.Area = search.Area;
                    newSearch.CallType = search.CallType;
                    newSearch.Prefix = search.Prefix;
                    newSearch.PBDID = search.PBDID;
                    returnObj = (IEnumerable<ProductBundleDetail>)GetProductBundleDetails_cSec2(newSearch);
                    if (returnObjValid.Count() == 0 && emptyErrors)
                        return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
                    returnObj = returnObjValid;
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, (IEnumerable<APIResults_Error_Output>)returnObj);
            }
            return Ok(returnObj);
        }
        private object UpdateProductBundleDetail_cSec2(UpdateProductBundleDetail search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_UpdateProductBundleDetail_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("PBDID", SqlDbType.VarChar).Value = search.PBDID;
                    com.Parameters.Add("ProductCode", SqlDbType.VarChar).Value = search.ProductCode;
                    com.Parameters.Add("Area", SqlDbType.VarChar).Value = search.Area;
                    com.Parameters.Add("CallType", SqlDbType.VarChar).Value = search.CallType;
                    com.Parameters.Add("Prefix", SqlDbType.VarChar).Value = search.Prefix;
                    com.Parameters.Add("TimeBand", SqlDbType.VarChar).Value = search.TimeBand;
                    com.Parameters.Add("InternalCalls", SqlDbType.VarChar).Value = search.InternalCalls;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    //List<Product> ret = dt.ToListCollection<Product>();
                    //return ret.AsEnumerable<Product>();
                    if (dt.Columns[0].ColumnName == "Code" && dt.Columns[1].ColumnName == "Message")
                        return ((dt.ToListCollection<APIResults_Error_Output>()).AsEnumerable<APIResults_Error_Output>());
                    return ((dt.ToListCollection<ProductBundleDetail>()).AsEnumerable<ProductBundleDetail>());
                }
                catch (Exception e)
                {
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }


        /// <summary>
        /// Get List of product Tiers
        /// </summary>
        /// <param name="search"></param>
        /// <returns>Product</returns>
        [Authorize]
        [HttpPost]
        [Route("api/GetProductBundleTiers")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object GetProductBundleTiers_Validation(GetProductBundleTiers search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Product))
                            search.Product = "";
                        //codeMessage = ProductCodeValidation(search.Product);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Floor.ToString()))
                            search.Floor = 0;
                        //codeMessage = AreaValidation(search.Area);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.PBTID))
                            search.PBTID = "";
                        //codeMessage = PBDIDValidation(search.PBDID);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }

            IEnumerable<ProductBundleTier> returnObj = (IEnumerable<ProductBundleTier>)GetProductBundleTiers_cSec2(search);
            if (returnObj.Count() == 0 && emptyErrors)
                return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
            return Ok(returnObj);
        }
        private object GetProductBundleTiers_cSec2(GetProductBundleTiers search)
        {
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_GetProductBundleTiers_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("Product", SqlDbType.VarChar).Value = search.Product;
                    com.Parameters.Add("Floor", SqlDbType.Decimal).Value = search.Floor;
                    com.Parameters.Add("PBTID", SqlDbType.VarChar).Value = search.PBTID;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    List<ProductBundleTier> ret = dt.ToListCollection<ProductBundleTier>();
                    return ret.AsEnumerable<ProductBundleTier>();
                }
                catch (Exception e)
                {
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }

        /// <summary>
        /// Create new product Tier
        /// </summary>
        /// <param name="search"></param>
        /// <returns>Product</returns>
        [Authorize]
        [HttpPost]
        [Route("api/CreateProductBundleTier")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object CreateProductBundleTier_Validation(CreateProductBundleTier search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.ProductCode))
                            search.ProductCode = "";
                        //codeMessage = ProductCodeValidation(search.ProductCode);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Floor.ToString()))
                            search.Floor = 0;
                        //codeMessage = AreaValidation(search.Area);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Ceiling.ToString()))
                            search.Ceiling = 0;
                        //codeMessage = CeilingValidation(search.Ceiling);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.PBTID))
                            search.PBTID = "";
                        //codeMessage = PBTIDValidation(search.PBTID);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.UnitType))
                            search.UnitType = "";
                        //codeMessage = UnitTypeValidation(search.UnitType);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.TierCost.ToString()))
                            search.TierCost = 0;
                        //codeMessage = TierCostValidation(search.TierCost);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.PPI.ToString()))
                            search.PPI = 0;
                        //codeMessage = CostValidation(search.PPI);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.PPV.ToString()))
                            search.PPV = 0;
                        //codeMessage = CostValidation(search.PPV);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }
            object returnObj = CreateProductBundleTier_cSec2(search);
            try
            {
                IEnumerable<ProductBundleTier> returnObjValid = (IEnumerable<ProductBundleTier>)returnObj;
                if (returnObjValid.Count() == 0 && emptyErrors)
                {
                    GetProductBundleTiers newSearch = new GetProductBundleTiers();
                    newSearch.Product = search.ProductCode;
                    newSearch.Floor = search.Floor;
                    newSearch.PBTID = search.PBTID;
                    returnObj = (IEnumerable<ProductBundleTier>)GetProductBundleTiers_cSec2(newSearch);
                    if (returnObjValid.Count() == 0 && emptyErrors)
                        return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
                    returnObj = returnObjValid;
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, (IEnumerable<APIResults_Error_Output>)returnObj);
            }
            return Ok(returnObj);
        }
        private object CreateProductBundleTier_cSec2(CreateProductBundleTier search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_CreateProductBundleTier_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("ProductCode", SqlDbType.VarChar).Value = search.ProductCode;
                    com.Parameters.Add("Floor", SqlDbType.Decimal).Value = search.Floor;
                    com.Parameters.Add("Ceiling", SqlDbType.Decimal).Value = search.Ceiling;
                    com.Parameters.Add("UnitType", SqlDbType.VarChar).Value = search.UnitType;
                    com.Parameters.Add("TierCost", SqlDbType.Decimal).Value = search.TierCost;
                    com.Parameters.Add("PPI", SqlDbType.Decimal).Value = search.PPI;
                    com.Parameters.Add("PPV", SqlDbType.Decimal).Value = search.PPV;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    //List<Product> ret = dt.ToListCollection<Product>();
                    //return ret.AsEnumerable<Product>();
                    if (dt.Columns[0].ColumnName == "Code" && dt.Columns[1].ColumnName == "Message")
                        return ((dt.ToListCollection<APIResults_Error_Output>()).AsEnumerable<APIResults_Error_Output>());
                    return ((dt.ToListCollection<ProductBundleTier>()).AsEnumerable<ProductBundleTier>());
                }
                catch (Exception e)
                {
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }

        /// <summary>
        /// Update product record using productcode as identifier
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("api/UpdateProductBundleTier")]
        [IPAddressFilter("127.0.0.1", IPAddressFilteringAction.Allow)]
        public object UpdateProductBundleTier_Validation(UpdateProductBundleTier search)
        {
            string Code = generalErrorCode;
            string Message = generalErrorMessage;

            string[] codeMessage = new string[] { Code, Message };
            try
            {
                if (search != null)
                {
                    codeMessage[0] = "200";
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.PBTID))
                            search.PBTID = "";
                        codeMessage = PBDIDValidation(search.PBTID);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Floor.ToString()))
                            search.Floor = -999;
                        //codeMessage = FloorValidation(search.Floor);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.Ceiling.ToString()))
                            search.Ceiling = -999;
                        //codeMessage = CeilingValidation(search.Ceiling);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.UnitType))
                            search.UnitType = defaultWildcardString;
                        //codeMessage = UnitTypeValidation(search.UnitType);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.TierCost.ToString()))
                            search.TierCost = -999;
                        //codeMessage = TierCostValidation(search.TierCost);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.PPI.ToString()))
                            search.PPI = -999;
                        //codeMessage = PPIValidation(search.PPI);
                    }
                    if (codeMessage[0].Equals("200"))
                    {
                        if (String.IsNullOrEmpty(search.PPV.ToString()))
                            search.PPV = -999;
                        //codeMessage = PPVValidation(search.PPV);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Code = codeMessage[0];
            Message = codeMessage[1];
            if (!Code.Equals("200"))
            {
                //return BadRequest(Message);
                return genErrorResult(Code, Message);
            }
            object returnObj = UpdateProductBundleTier_cSec2(search);
            try
            {
                IEnumerable<ProductBundleTier> returnObjValid = (IEnumerable<ProductBundleTier>)returnObj;
                if (returnObjValid.Count() == 0 && emptyErrors)
                {
                    GetProductBundleTiers newSearch = new GetProductBundleTiers();
                    newSearch.Product = search.ProductCode;
                    newSearch.Floor = search.Floor;
                    newSearch.PBTID = search.PBTID;
                    returnObj = (IEnumerable<ProductBundleTier>)GetProductBundleTiers_cSec2(newSearch);
                    if (returnObjValid.Count() == 0 && emptyErrors)
                        return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
                    returnObj = returnObjValid;
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, (IEnumerable<APIResults_Error_Output>)returnObj);
            }
            return Ok(returnObj);
        }
        private object UpdateProductBundleTier_cSec2(UpdateProductBundleTier search)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_UpdateProductBundleTier_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("PBTID", SqlDbType.VarChar).Value = search.PBTID;
                    com.Parameters.Add("Floor", SqlDbType.Decimal).Value = search.Floor;
                    com.Parameters.Add("Ceiling", SqlDbType.Decimal).Value = search.Ceiling;
                    com.Parameters.Add("UnitType", SqlDbType.VarChar).Value = search.UnitType;
                    com.Parameters.Add("TierCost", SqlDbType.Decimal).Value = search.TierCost;
                    com.Parameters.Add("PPI", SqlDbType.Decimal).Value = search.PPI;
                    com.Parameters.Add("PPV", SqlDbType.Decimal).Value = search.PPV;

                    com.Parameters.Add("User", SqlDbType.VarChar).Value = ControllerContext.RequestContext.Principal.Identity.Name;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    //List<Product> ret = dt.ToListCollection<Product>();
                    //return ret.AsEnumerable<Product>();
                    if (dt.Columns[0].ColumnName == "Code" && dt.Columns[1].ColumnName == "Message")
                        return ((dt.ToListCollection<APIResults_Error_Output>()).AsEnumerable<APIResults_Error_Output>());
                    return ((dt.ToListCollection<ProductBundleTier>()).AsEnumerable<ProductBundleTier>());
                }
                catch (Exception e)
                {
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }

        /*

                [Authorize]
                [HttpPost]
                [Route("api/GetInvoiceList")]
                public object GetInvoiceList_Validation(APIResults_InvList_Input search)
                {
                    string Code = generalErrorCode;
                    string Message = generalErrorMessage;
                    string Date = defaultDate;
                    string Customer = "";
                    string[] codeMessage = new string[] { Code, Message };
                    try
                    {
                        if (search != null)
                        {
                            if (!String.IsNullOrEmpty(search.Customer))
                                Customer = search.Customer;
                            codeMessage = AccountNumberValidation(Customer);
                            if (codeMessage[0].Equals("200"))
                            {
                                if (!String.IsNullOrEmpty(search.Date))
                                    Date = search.Date;
                                codeMessage = DateValidation(Date);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    Code = codeMessage[0];
                    Message = codeMessage[1];
                    if (!Code.Equals("200"))
                    {
                        //return BadRequest(Message);
                        return genErrorResult(Code, Message);
                    }
                    search.Customer = Customer;
                    search.Date = Date;
                    IEnumerable<APIResults_InvList_Output> returnObj = (IEnumerable<APIResults_InvList_Output>)GetInvoiceList_cSec2(search);
                    if (returnObj.Count() == 0 && emptyErrors)
                        return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
                    return Ok(returnObj);
                }
                private object GetInvoiceList_cSec2(APIResults_InvList_Input search)
                {
                    //string DBConnectionString = "";
                    using (SqlConnection con = DbUtil.GetConnection())
                    {
                        try
                        {
                            SqlCommand com = new SqlCommand("AuthAPI_GetInvoiceList_POST", con);
                            com.CommandType = CommandType.StoredProcedure;
                            SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                            RetVal.Direction = ParameterDirection.ReturnValue;
                            com.Parameters.Add("Customer", SqlDbType.VarChar).Value = search.Customer;
                            com.Parameters.Add("Date", SqlDbType.VarChar).Value = search.Date;
                            SqlDataAdapter da = new SqlDataAdapter(com);
                            con.Open();
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            da.Dispose();
                            //      logger.Info("customers_get:, return={0}", RetVal.Value);
                            DataTable dt = ds.Tables[0];
                            List<APIResults_InvList_Output> ret = dt.ToListCollection<APIResults_InvList_Output>();
                            return ret.AsEnumerable<APIResults_InvList_Output>();
                        }
                        catch (Exception e)
                        {
                            //return (new List<APIResults_InvList_Output>()).AsEnumerable<APIResults_InvList_Output>();
                            return genErrorResult(generalErrorCode, generalErrorMessage);
                        }
                    }
                }

                [Authorize]
                [HttpPost]
                [Route("api/GetInvoiceLines")]
                public object GetInvoiceLines_Validation(APIResults_InvList_Input search)
                {
                    string Code = generalErrorCode;
                    string Message = generalErrorMessage;
                    string Date = defaultDate;
                    string Customer = "";
                    string[] codeMessage = new string[] { Code, Message };
                    try
                    {
                        if (search != null)
                        {
                            if (!String.IsNullOrEmpty(search.Customer))
                                Customer = search.Customer;
                            codeMessage = AccountNumberValidation(Customer);
                            if (codeMessage[0].Equals("200"))
                            {
                                if (!String.IsNullOrEmpty(search.Date))
                                    Date = search.Date;
                                codeMessage = DateValidation(Date);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    Code = codeMessage[0];
                    Message = codeMessage[1];
                    if (!Code.Equals("200"))
                    {
                        //return BadRequest(Message);
                        return genErrorResult(Code, Message);
                    }
                    search.Customer = Customer;
                    search.Date = Date;
                    IEnumerable<APIResults_InvList_Output> returnObj = (IEnumerable<APIResults_InvList_Output>)GetInvoiceLines_cSec2(search);
                    if (returnObj.Count() == 0 && emptyErrors)
                        return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
                    return Ok(returnObj);
                }
                private object GetInvoiceLines_cSec2(APIResults_InvList_Input search)
                {
                    //string DBConnectionString = "";
                    using (SqlConnection con = DbUtil.GetConnection())
                    {
                        try
                        {
                            SqlCommand com = new SqlCommand("AuthAPI_GetInvoiceLines_POST", con);
                            com.CommandType = CommandType.StoredProcedure;
                            SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                            RetVal.Direction = ParameterDirection.ReturnValue;
                            com.Parameters.Add("Customer", SqlDbType.VarChar).Value = search.Customer;
                            com.Parameters.Add("Date", SqlDbType.VarChar).Value = search.Date;
                            SqlDataAdapter da = new SqlDataAdapter(com);
                            con.Open();
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            da.Dispose();
                            //      logger.Info("customers_get:, return={0}", RetVal.Value);
                            DataTable dt = ds.Tables[0];
                            List<APIResults_InvList_Output> ret = dt.ToListCollection<APIResults_InvList_Output>();
                            return ret.AsEnumerable<APIResults_InvList_Output>();
                        }
                        catch (Exception e)
                        {
                            //return (new List<APIResults_InvList_Output>()).AsEnumerable<APIResults_InvList_Output>();
                            return genErrorResult(generalErrorCode, generalErrorMessage);
                        }
                    }
                }

                [Authorize]
                [HttpPost]
                [Route("api/GetUnbilledCDRs")]
                public object GetUnbilledCDRs_Validation(APIResults_InvList_Input search)
                {
                    string Code = generalErrorCode;
                    string Message = generalErrorMessage;
                    string Date = defaultDate;
                    string Customer = "";
                    string[] codeMessage = new string[] { Code, Message };
                    try
                    {
                        if (search != null)
                        {
                            if (!String.IsNullOrEmpty(search.Customer))
                                Customer = search.Customer;
                            codeMessage = AccountNumberValidation(Customer);
                            if (codeMessage[0].Equals("200"))
                            {
                                if (!String.IsNullOrEmpty(search.Date))
                                    Date = search.Date;
                                codeMessage = DateValidation(Date);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    Code = codeMessage[0];
                    Message = codeMessage[1];
                    if (!Code.Equals("200"))
                    {
                        //return BadRequest(Message);
                        return genErrorResult(Code, Message);
                    }
                    search.Customer = Customer;
                    search.Date = Date;
                    IEnumerable<APIResults_InvList_Output> returnObj = (IEnumerable<APIResults_InvList_Output>)GetUnbilledCDRs_cSec2(search);
                    if (returnObj.Count() == 0 && emptyErrors)
                        return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
                    return Ok(returnObj);
                }
                private object GetUnbilledCDRs_cSec2(APIResults_InvList_Input search)
                {
                    //string DBConnectionString = "";
                    using (SqlConnection con = DbUtil.GetConnection())
                    {
                        try
                        {
                            SqlCommand com = new SqlCommand("AuthAPI_GetUnbilledCDRs_POST", con);
                            com.CommandType = CommandType.StoredProcedure;
                            SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                            RetVal.Direction = ParameterDirection.ReturnValue;
                            com.Parameters.Add("Customer", SqlDbType.VarChar).Value = search.Customer;
                            com.Parameters.Add("Date", SqlDbType.VarChar).Value = search.Date;
                            SqlDataAdapter da = new SqlDataAdapter(com);
                            con.Open();
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            da.Dispose();
                            //      logger.Info("customers_get:, return={0}", RetVal.Value);
                            DataTable dt = ds.Tables[0];
                            List<APIResults_InvList_Output> ret = dt.ToListCollection<APIResults_InvList_Output>();
                            return ret.AsEnumerable<APIResults_InvList_Output>();
                        }
                        catch (Exception e)
                        {
                            //return (new List<APIResults_InvList_Output>()).AsEnumerable<APIResults_InvList_Output>();
                            return genErrorResult(generalErrorCode, generalErrorMessage);
                        }
                    }
                }

                [Authorize]
                [HttpPost]
                [Route("api/GetBilledCDRs")]
                public object GetBilledCDRs_Validation(APIResults_InvList_Input search)
                {
                    string Code = generalErrorCode;
                    string Message = generalErrorMessage;
                    string Date = defaultDate;
                    string Customer = "";
                    string[] codeMessage = new string[] { Code, Message };
                    try
                    {
                        if (search != null)
                        {
                            if (!String.IsNullOrEmpty(search.Customer))
                                Customer = search.Customer;
                            codeMessage = AccountNumberValidation(Customer);
                            if (codeMessage[0].Equals("200"))
                            {
                                if (!String.IsNullOrEmpty(search.Date))
                                    Date = search.Date;
                                codeMessage = DateValidation(Date);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    Code = codeMessage[0];
                    Message = codeMessage[1];
                    if (!Code.Equals("200"))
                    {
                        //return BadRequest(Message);
                        return genErrorResult(Code, Message);
                    }
                    search.Customer = Customer;
                    search.Date = Date;
                    IEnumerable<APIResults_InvList_Output> returnObj = (IEnumerable<APIResults_InvList_Output>)GetBilledCDRs_cSec2(search);
                    if (returnObj.Count() == 0 && emptyErrors)
                        return genEmptyResult(notFoundErrorCode, notFoundErrorMessage);
                    return Ok(returnObj);
                }
                private object GetBilledCDRs_cSec2(APIResults_InvList_Input search)
                {
                    //string DBConnectionString = "";
                    using (SqlConnection con = DbUtil.GetConnection())
                    {
                        try
                        {
                            SqlCommand com = new SqlCommand("AuthAPI_GetBilledCDRs_POST", con);
                            com.CommandType = CommandType.StoredProcedure;
                            SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                            RetVal.Direction = ParameterDirection.ReturnValue;
                            com.Parameters.Add("Customer", SqlDbType.VarChar).Value = search.Customer;
                            com.Parameters.Add("Date", SqlDbType.VarChar).Value = search.Date;
                            SqlDataAdapter da = new SqlDataAdapter(com);
                            con.Open();
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            da.Dispose();
                            //      logger.Info("customers_get:, return={0}", RetVal.Value);
                            DataTable dt = ds.Tables[0];
                            List<APIResults_InvList_Output> ret = dt.ToListCollection<APIResults_InvList_Output>();
                            return ret.AsEnumerable<APIResults_InvList_Output>();
                        }
                        catch (Exception e)
                        {
                            //return (new List<APIResults_InvList_Output>()).AsEnumerable<APIResults_InvList_Output>();
                            return genErrorResult(generalErrorCode, generalErrorMessage);
                        }
                    }
                }
        */

        private object SQLBuilder(string sProc, string[] paramHeaders, string[] paramValues)
        {
            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection())
            {
                try
                {
                    SqlCommand com = new SqlCommand(sProc, con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    for (int i = 0; i < paramHeaders.Length; i++)
                    {
                        com.Parameters.Add(paramHeaders[i], SqlDbType.VarChar).Value = paramValues[i];
                    }
 
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    return ds.Tables[0];
                }
                catch (Exception e)
                {
                    return genErrorResult(generalErrorCode, generalErrorMessage);
                }
            }
        }

        /*private APIResult_Error_Output DBResultsChecker(DataTable dt)
        {
            try
            {
                List<APIResults_Error_Output> ret = dt.ToListCollection<APIResults_Error_Output>();
                if (ret.Count == 1)
                {
                    APIResults_Error_Output resCheck = ret[0];
                    if (!String.IsNullOrEmpty(resCheck.Code))
                    {
                        List<APIResults_Error_Output> retCM = dt.ToListCollection<APIResults_Error_Output>();
                        return retCM.AsEnumerable<APIResults_Error_Output>();
                    }
                    return ret.AsEnumerable<CustomerProduct>();
                }
               

            }
            catch (Exception e)
            {
                return genErrorResult(generalErrorCode, generalErrorMessage);
            }*/
    }






    public enum IPAddressFilteringAction
    {
        Allow,
        Restrict
    }
    public class IPAddressFilterAttribute : AuthorizeAttribute
    {
        #region Fields
        private IEnumerable<IPAddress> ipAddresses;
        private IEnumerable<IPAddressRange> ipAddressRanges;
        private IPAddressFilteringAction filteringType;
        #endregion

        #region Properties
        public IEnumerable<IPAddress> IPAddresses
        {
            get
            {
                return this.ipAddresses;
            }
        }

        public IEnumerable<IPAddressRange> IPAddressRanges
        {
            get
            {
                return this.ipAddressRanges;
            }
        }
        #endregion

        #region Constructors
        public IPAddressFilterAttribute(string ipAddress, IPAddressFilteringAction filteringType)
           : this(new IPAddress[] { IPAddress.Parse(ipAddress) }, filteringType)
        {

        }

        public IPAddressFilterAttribute(IPAddress ipAddress, IPAddressFilteringAction filteringType)
            : this(new IPAddress[] { ipAddress }, filteringType)
        {

        }

        public IPAddressFilterAttribute(IEnumerable<string> ipAddresses, IPAddressFilteringAction filteringType)
            : this(ipAddresses.Select(a => IPAddress.Parse(a)), filteringType)
        {

        }

        public IPAddressFilterAttribute(IEnumerable<IPAddress> ipAddresses, IPAddressFilteringAction filteringType)
        {
            this.ipAddresses = ipAddresses;
            this.filteringType = filteringType;
        }

        public IPAddressFilterAttribute(string ipAddressRangeStart, string ipAddressRangeEnd, IPAddressFilteringAction filteringType)
            : this(new IPAddressRange[] { new IPAddressRange(ipAddressRangeStart, ipAddressRangeEnd) }, filteringType)
        {

        }

        public IPAddressFilterAttribute(IPAddressRange ipAddressRange, IPAddressFilteringAction filteringType)
            : this(new IPAddressRange[] { ipAddressRange }, filteringType)
        {

        }

        public IPAddressFilterAttribute(IEnumerable<IPAddressRange> ipAddressRanges, IPAddressFilteringAction filteringType)
        {
            this.ipAddressRanges = ipAddressRanges;
            this.filteringType = filteringType;
        }

        #endregion

        protected override bool IsAuthorized(HttpActionContext context)
        {
            string ipAddressString = ((HttpContextWrapper)context.Request.Properties["MS_HttpContext"]).Request.UserHostName;
            string userName = context.RequestContext.Principal.Identity.Name;
            string apiMethod = context.Request.RequestUri.AbsolutePath.ToString();
            return IsIPAddressAllowed(ipAddressString, userName, apiMethod);
            //return IsIPAddressAllowed(ipAddressString, userName);
        }

        private bool IsIPAddressAllowed(string ipAddressString)
        {
            IPAddress ipAddress = IPAddress.Parse(ipAddressString);

            if (this.filteringType == IPAddressFilteringAction.Allow)
            {
                if (this.ipAddresses != null && this.ipAddresses.Any() &&
                    !IsIPAddressInList(ipAddressString.Trim()))
                {
                    return false;
                }
                else if (this.ipAddressRanges != null && this.ipAddressRanges.Any() &&
                    !this.ipAddressRanges.Where(r => ipAddress.IsInRange(r.StartIPAddress, r.EndIPAddress)).Any())
                {
                    return false;
                }

            }
            else
            {
                if (this.ipAddresses != null && this.ipAddresses.Any() &&
                   IsIPAddressInList(ipAddressString.Trim()))
                {
                    return false;
                }
                else if (this.ipAddressRanges != null && this.ipAddressRanges.Any() &&
                    this.ipAddressRanges.Where(r => ipAddress.IsInRange(r.StartIPAddress, r.EndIPAddress)).Any())
                {
                    return false;
                }

            }

            return true;

        }
        private bool IsIPAddressAllowed(string ipAddressString,string username)
        {
            IPAddress ipAddress = IPAddress.Parse(ipAddressString);

            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection_Target("data source = 192.168.2.192; initial catalog = QSSConnect; user id = QSConnAPI;password=J?Z!#rp?Y47xu=pD;Integrated Security=False"))
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_UserIPCheck_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("User", SqlDbType.VarChar).Value = username;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = ipAddressString;
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        string name = row["SecurityCheck"].ToString();
                        if (name.Equals("Fail"))
                            return false;
                    }                                          
                }
                catch (Exception e)
                {
                    return false;
                }

            }

            return true;

        }

        private bool IsIPAddressAllowed(string ipAddressString, string username,string apiMethod)
        {
            IPAddress ipAddress = IPAddress.Parse(ipAddressString);

            //string DBConnectionString = "";
            using (SqlConnection con = DbUtil.GetConnection_Target("data source = 192.168.2.192; initial catalog = QSSConnect; user id = QSConnAPI;password=J?Z!#rp?Y47xu=pD;Integrated Security=False"))
            {
                try
                {
                    SqlCommand com = new SqlCommand("AuthAPI_UserIPCheck_POST", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter RetVal = com.Parameters.Add("RetVal", SqlDbType.VarChar);
                    RetVal.Direction = ParameterDirection.ReturnValue;
                    com.Parameters.Add("User", SqlDbType.VarChar).Value = username;
                    com.Parameters.Add("IP", SqlDbType.VarChar).Value = ipAddressString;
                    com.Parameters.Add("APIMethod", SqlDbType.VarChar).Value = apiMethod;
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    //      logger.Info("customers_get:, return={0}", RetVal.Value);
                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        string name = row["SecurityCheck"].ToString();
                        if (name.Equals("Fail"))
                            return false;
                    }
                }
                catch (Exception e)
                {
                    return false;
                }

            }

            return true;

        }

        private bool IsIPAddressInList(string ipAddress)
        {
            if (!string.IsNullOrWhiteSpace(ipAddress))
            {
                IEnumerable<string> addresses = this.ipAddresses.Select(a => a.ToString());
                return addresses.Where(a => a.Trim().Equals(ipAddress, StringComparison.InvariantCultureIgnoreCase)).Any();
            }
            return false;
        }

    }
    public static class IPAddressExtensions
    {
        public static bool IsInRange(this IPAddress address, IPAddress start, IPAddress end)
        {

            AddressFamily addressFamily = start.AddressFamily;
            byte[] lowerBytes = start.GetAddressBytes();
            byte[] upperBytes = end.GetAddressBytes();

            if (address.AddressFamily != addressFamily)
            {
                return false;
            }

            byte[] addressBytes = address.GetAddressBytes();

            bool lowerBoundary = true, upperBoundary = true;

            for (int i = 0; i < lowerBytes.Length &&
                (lowerBoundary || upperBoundary); i++)
            {
                if ((lowerBoundary && addressBytes[i] < lowerBytes[i]) ||
                    (upperBoundary && addressBytes[i] > upperBytes[i]))
                {
                    return false;
                }

                lowerBoundary &= (addressBytes[i] == lowerBytes[i]);
                upperBoundary &= (addressBytes[i] == upperBytes[i]);
            }

            return true;
        }

    }

    public class IPAddressRange
    {
        private IPAddress startIPAddress;
        private IPAddress endIPAddress;

        public IPAddress StartIPAddress
        {
            get
            {
                return this.startIPAddress;
            }
        }

        public IPAddress EndIPAddress
        {
            get
            {
                return this.endIPAddress;
            }
        }

        public IPAddressRange(string startIPAddress, string endIPAddress)
            : this(IPAddress.Parse(startIPAddress), IPAddress.Parse(endIPAddress))
        {

        }
        public IPAddressRange(IPAddress startIPAddress, IPAddress endIPAddress)
        {
            this.startIPAddress = startIPAddress;
            this.endIPAddress = endIPAddress;
        }
    }

}
