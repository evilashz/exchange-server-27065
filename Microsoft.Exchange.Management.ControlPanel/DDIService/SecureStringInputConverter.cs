using System;
using Microsoft.Exchange.Extensions;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x0200015D RID: 349
	public class SecureStringInputConverter : IInputConverter, IDDIConverter
	{
		// Token: 0x060021B6 RID: 8630 RVA: 0x0006567C File Offset: 0x0006387C
		public bool CanConvert(object sourceObject)
		{
			return sourceObject is string;
		}

		// Token: 0x060021B7 RID: 8631 RVA: 0x00065688 File Offset: 0x00063888
		public object Convert(object sourceObject)
		{
			string password = sourceObject as string;
			return password.ConvertToSecureString();
		}
	}
}
