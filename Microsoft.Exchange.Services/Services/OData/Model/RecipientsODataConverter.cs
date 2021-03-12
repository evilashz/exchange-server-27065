using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Services.OData.Web;
using Microsoft.OData.Core;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E78 RID: 3704
	internal class RecipientsODataConverter : IODataPropertyValueConverter
	{
		// Token: 0x06006058 RID: 24664 RVA: 0x0012CDD1 File Offset: 0x0012AFD1
		public object FromODataPropertyValue(object odataPropertyValue)
		{
			return RecipientsODataConverter.ODataCollectionValueToRecipients((ODataCollectionValue)odataPropertyValue);
		}

		// Token: 0x06006059 RID: 24665 RVA: 0x0012CDDE File Offset: 0x0012AFDE
		public object ToODataPropertyValue(object rawValue)
		{
			return RecipientsODataConverter.RecipientsToODataCollectionValue(((Recipient[])rawValue) ?? new Recipient[0]);
		}

		// Token: 0x0600605A RID: 24666 RVA: 0x0012CE00 File Offset: 0x0012B000
		internal static Recipient[] ODataCollectionValueToRecipients(ODataCollectionValue collection)
		{
			IEnumerable<Recipient> source = from ODataComplexValue x in collection.Items
			select RecipientODataConverter.ODataValueToRecipient(x);
			return source.ToArray<Recipient>();
		}

		// Token: 0x0600605B RID: 24667 RVA: 0x0012CE4C File Offset: 0x0012B04C
		internal static ODataCollectionValue RecipientsToODataCollectionValue(Recipient[] recipients)
		{
			ODataCollectionValue odataCollectionValue = new ODataCollectionValue();
			odataCollectionValue.TypeName = typeof(Recipient).MakeODataCollectionTypeName();
			odataCollectionValue.Items = Array.ConvertAll<Recipient, ODataValue>(recipients, (Recipient x) => RecipientODataConverter.RecipientToODataValue(x));
			return odataCollectionValue;
		}
	}
}
