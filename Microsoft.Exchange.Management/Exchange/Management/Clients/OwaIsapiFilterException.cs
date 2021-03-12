using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Clients
{
	// Token: 0x02000DD9 RID: 3545
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class OwaIsapiFilterException : LocalizedException
	{
		// Token: 0x0600A429 RID: 42025 RVA: 0x00283C03 File Offset: 0x00281E03
		public OwaIsapiFilterException(string errorMessage, int hresult) : base(Strings.OwaIsapiFilterException(errorMessage, hresult))
		{
			this.errorMessage = errorMessage;
			this.hresult = hresult;
		}

		// Token: 0x0600A42A RID: 42026 RVA: 0x00283C20 File Offset: 0x00281E20
		public OwaIsapiFilterException(string errorMessage, int hresult, Exception innerException) : base(Strings.OwaIsapiFilterException(errorMessage, hresult), innerException)
		{
			this.errorMessage = errorMessage;
			this.hresult = hresult;
		}

		// Token: 0x0600A42B RID: 42027 RVA: 0x00283C40 File Offset: 0x00281E40
		protected OwaIsapiFilterException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorMessage = (string)info.GetValue("errorMessage", typeof(string));
			this.hresult = (int)info.GetValue("hresult", typeof(int));
		}

		// Token: 0x0600A42C RID: 42028 RVA: 0x00283C95 File Offset: 0x00281E95
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorMessage", this.errorMessage);
			info.AddValue("hresult", this.hresult);
		}

		// Token: 0x170035E2 RID: 13794
		// (get) Token: 0x0600A42D RID: 42029 RVA: 0x00283CC1 File Offset: 0x00281EC1
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
		}

		// Token: 0x170035E3 RID: 13795
		// (get) Token: 0x0600A42E RID: 42030 RVA: 0x00283CC9 File Offset: 0x00281EC9
		public int Hresult
		{
			get
			{
				return this.hresult;
			}
		}

		// Token: 0x04005F48 RID: 24392
		private readonly string errorMessage;

		// Token: 0x04005F49 RID: 24393
		private readonly int hresult;
	}
}
