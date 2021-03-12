using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011EF RID: 4591
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TUC_MakeCallError : LocalizedException
	{
		// Token: 0x0600B986 RID: 47494 RVA: 0x002A60B0 File Offset: 0x002A42B0
		public TUC_MakeCallError(string host, string error) : base(Strings.MakeCallError(host, error))
		{
			this.host = host;
			this.error = error;
		}

		// Token: 0x0600B987 RID: 47495 RVA: 0x002A60CD File Offset: 0x002A42CD
		public TUC_MakeCallError(string host, string error, Exception innerException) : base(Strings.MakeCallError(host, error), innerException)
		{
			this.host = host;
			this.error = error;
		}

		// Token: 0x0600B988 RID: 47496 RVA: 0x002A60EC File Offset: 0x002A42EC
		protected TUC_MakeCallError(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.host = (string)info.GetValue("host", typeof(string));
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x0600B989 RID: 47497 RVA: 0x002A6141 File Offset: 0x002A4341
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("host", this.host);
			info.AddValue("error", this.error);
		}

		// Token: 0x17003A47 RID: 14919
		// (get) Token: 0x0600B98A RID: 47498 RVA: 0x002A616D File Offset: 0x002A436D
		public string Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x17003A48 RID: 14920
		// (get) Token: 0x0600B98B RID: 47499 RVA: 0x002A6175 File Offset: 0x002A4375
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x04006462 RID: 25698
		private readonly string host;

		// Token: 0x04006463 RID: 25699
		private readonly string error;
	}
}
