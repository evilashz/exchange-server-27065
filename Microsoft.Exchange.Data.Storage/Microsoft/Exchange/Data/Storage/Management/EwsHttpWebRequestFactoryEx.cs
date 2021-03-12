using System;
using System.Net;
using System.Net.Security;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x020009C6 RID: 2502
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class EwsHttpWebRequestFactoryEx : IEwsHttpWebRequestFactory
	{
		// Token: 0x06005C71 RID: 23665 RVA: 0x00181538 File Offset: 0x0017F738
		IEwsHttpWebRequest IEwsHttpWebRequestFactory.CreateRequest(Uri uri)
		{
			return new EwsHttpWebRequestEx(uri)
			{
				ServerCertificateValidationCallback = this.ServerCertificateValidationCallback
			};
		}

		// Token: 0x06005C72 RID: 23666 RVA: 0x00181559 File Offset: 0x0017F759
		IEwsHttpWebResponse IEwsHttpWebRequestFactory.CreateExceptionResponse(WebException exception)
		{
			return this.originalFactory.CreateExceptionResponse(exception);
		}

		// Token: 0x17001962 RID: 6498
		// (get) Token: 0x06005C73 RID: 23667 RVA: 0x00181567 File Offset: 0x0017F767
		// (set) Token: 0x06005C74 RID: 23668 RVA: 0x0018156F File Offset: 0x0017F76F
		public RemoteCertificateValidationCallback ServerCertificateValidationCallback { get; set; }

		// Token: 0x040032E6 RID: 13030
		private readonly IEwsHttpWebRequestFactory originalFactory = new EwsHttpWebRequestFactory();
	}
}
