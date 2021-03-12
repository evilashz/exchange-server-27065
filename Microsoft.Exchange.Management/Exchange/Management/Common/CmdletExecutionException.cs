using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Common
{
	// Token: 0x02000FD6 RID: 4054
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CmdletExecutionException : LocalizedException
	{
		// Token: 0x0600ADFD RID: 44541 RVA: 0x00292701 File Offset: 0x00290901
		public CmdletExecutionException(string cmdlet) : base(Strings.CmdletExecutionError(cmdlet))
		{
			this.cmdlet = cmdlet;
		}

		// Token: 0x0600ADFE RID: 44542 RVA: 0x00292716 File Offset: 0x00290916
		public CmdletExecutionException(string cmdlet, Exception innerException) : base(Strings.CmdletExecutionError(cmdlet), innerException)
		{
			this.cmdlet = cmdlet;
		}

		// Token: 0x0600ADFF RID: 44543 RVA: 0x0029272C File Offset: 0x0029092C
		protected CmdletExecutionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.cmdlet = (string)info.GetValue("cmdlet", typeof(string));
		}

		// Token: 0x0600AE00 RID: 44544 RVA: 0x00292756 File Offset: 0x00290956
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("cmdlet", this.cmdlet);
		}

		// Token: 0x170037C2 RID: 14274
		// (get) Token: 0x0600AE01 RID: 44545 RVA: 0x00292771 File Offset: 0x00290971
		public string Cmdlet
		{
			get
			{
				return this.cmdlet;
			}
		}

		// Token: 0x04006128 RID: 24872
		private readonly string cmdlet;
	}
}
