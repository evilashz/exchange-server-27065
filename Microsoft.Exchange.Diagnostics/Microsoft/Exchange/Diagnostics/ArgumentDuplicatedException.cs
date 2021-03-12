using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200041B RID: 1051
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ArgumentDuplicatedException : DiagnosticArgumentException
	{
		// Token: 0x0600195E RID: 6494 RVA: 0x0005F9C1 File Offset: 0x0005DBC1
		public ArgumentDuplicatedException(string msg) : base(DiagnosticsResources.ArgumentDuplicated(msg))
		{
			this.msg = msg;
		}

		// Token: 0x0600195F RID: 6495 RVA: 0x0005F9D6 File Offset: 0x0005DBD6
		public ArgumentDuplicatedException(string msg, Exception innerException) : base(DiagnosticsResources.ArgumentDuplicated(msg), innerException)
		{
			this.msg = msg;
		}

		// Token: 0x06001960 RID: 6496 RVA: 0x0005F9EC File Offset: 0x0005DBEC
		protected ArgumentDuplicatedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.msg = (string)info.GetValue("msg", typeof(string));
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x0005FA16 File Offset: 0x0005DC16
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("msg", this.msg);
		}

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x06001962 RID: 6498 RVA: 0x0005FA31 File Offset: 0x0005DC31
		public string Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x04001DEE RID: 7662
		private readonly string msg;
	}
}
