using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000E2 RID: 226
	[DataContract]
	public abstract class ResultSizeFilter : WebServiceParameters
	{
		// Token: 0x06001E1C RID: 7708 RVA: 0x0005B2A4 File Offset: 0x000594A4
		protected ResultSizeFilter()
		{
			this.OnDeserializing(default(StreamingContext));
		}

		// Token: 0x06001E1D RID: 7709 RVA: 0x0005B2C6 File Offset: 0x000594C6
		[OnDeserializing]
		private void OnDeserializing(StreamingContext context)
		{
			this.ResultSize = 500;
		}

		// Token: 0x1700199C RID: 6556
		// (get) Token: 0x06001E1E RID: 7710 RVA: 0x0005B2D3 File Offset: 0x000594D3
		// (set) Token: 0x06001E1F RID: 7711 RVA: 0x0005B2E5 File Offset: 0x000594E5
		[DataMember]
		public int ResultSize
		{
			get
			{
				return (int)base["ResultSize"];
			}
			set
			{
				base["ResultSize"] = value;
			}
		}

		// Token: 0x04001BFF RID: 7167
		public const int ResultSizeLimit = 500;

		// Token: 0x04001C00 RID: 7168
		public const string RbacParameters = "?ResultSize";
	}
}
