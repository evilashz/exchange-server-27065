using System;
using System.IO;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x02000236 RID: 566
	internal class XsoSMSContentProperty : XsoContent14Property
	{
		// Token: 0x06001506 RID: 5382 RVA: 0x0007B648 File Offset: 0x00079848
		protected override void InternalCopyFromModified(IProperty srcProperty)
		{
			base.InternalCopyFromModified(srcProperty);
			MessageItem messageItem = (MessageItem)base.XsoItem;
			StreamReader streamReader = new StreamReader(((IContentProperty)srcProperty).Body);
			char[] array = new char[160];
			int length = streamReader.ReadBlock(array, 0, array.Length);
			messageItem.Subject = new string(array, 0, length);
		}
	}
}
