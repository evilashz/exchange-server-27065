using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003AC RID: 940
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class PreserveInspectorLogsException : TransientException
	{
		// Token: 0x060027AD RID: 10157 RVA: 0x000B65F2 File Offset: 0x000B47F2
		public PreserveInspectorLogsException(string errorText) : base(ReplayStrings.PreserveInspectorLogsError(errorText))
		{
			this.errorText = errorText;
		}

		// Token: 0x060027AE RID: 10158 RVA: 0x000B6607 File Offset: 0x000B4807
		public PreserveInspectorLogsException(string errorText, Exception innerException) : base(ReplayStrings.PreserveInspectorLogsError(errorText), innerException)
		{
			this.errorText = errorText;
		}

		// Token: 0x060027AF RID: 10159 RVA: 0x000B661D File Offset: 0x000B481D
		protected PreserveInspectorLogsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorText = (string)info.GetValue("errorText", typeof(string));
		}

		// Token: 0x060027B0 RID: 10160 RVA: 0x000B6647 File Offset: 0x000B4847
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorText", this.errorText);
		}

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x060027B1 RID: 10161 RVA: 0x000B6662 File Offset: 0x000B4862
		public string ErrorText
		{
			get
			{
				return this.errorText;
			}
		}

		// Token: 0x040013A4 RID: 5028
		private readonly string errorText;
	}
}
