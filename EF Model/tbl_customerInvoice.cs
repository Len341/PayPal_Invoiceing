//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PayPal_Invoiceing.EF_Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_customerInvoice
    {
        public long idtbl_customerInvoice { get; set; }
        public string customerEmail { get; set; }
        public double amount { get; set; }
        public Nullable<System.DateTime> System_Date { get; set; }
        public string customerFname { get; set; }
        public string customerLname { get; set; }
        public string invoicerLname { get; set; }
        public string invoicerFname { get; set; }
        public string invoicerStreet { get; set; }
        public string invoicerTown { get; set; }
        public string invoicerState { get; set; }
        public string invoicerPostCode { get; set; }
        public string invoicerCountry { get; set; }
        public string invoicerCountryCode { get; set; }
        public string invoicerPhone { get; set; }
        public string invoicerWebsite { get; set; }
        public string invoicerAdditionalInfo { get; set; }
        public string invoicerLogoUrl { get; set; }
        public string invoicerTaxId { get; set; }
        public string invoicerRefNum { get; set; }
        public string customerCountryCode { get; set; }
        public string customerPhone { get; set; }
    }
}
