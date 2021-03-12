using System;
using System.Net;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x0200003F RID: 63
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	internal class DeleteAttribute : HttpMethodAttribute
	{
		// Token: 0x0600022E RID: 558 RVA: 0x00007D00 File Offset: 0x00005F00
		public DeleteAttribute() : base("Delete")
		{
			base.StatusCode = HttpStatusCode.NoContent;
		}
	}
}
