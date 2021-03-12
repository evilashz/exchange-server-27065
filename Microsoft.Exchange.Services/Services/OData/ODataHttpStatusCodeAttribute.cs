using System;
using System.Net;

namespace Microsoft.Exchange.Services.OData
{
	// Token: 0x02000E3A RID: 3642
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	internal class ODataHttpStatusCodeAttribute : Attribute
	{
		// Token: 0x06005DE6 RID: 24038 RVA: 0x00124399 File Offset: 0x00122599
		public ODataHttpStatusCodeAttribute(HttpStatusCode statusCode)
		{
			this.HttpStatusCode = statusCode;
		}

		// Token: 0x17001545 RID: 5445
		// (get) Token: 0x06005DE7 RID: 24039 RVA: 0x001243A8 File Offset: 0x001225A8
		// (set) Token: 0x06005DE8 RID: 24040 RVA: 0x001243B0 File Offset: 0x001225B0
		public HttpStatusCode HttpStatusCode { get; set; }
	}
}
