using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x0200004B RID: 75
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UnexpectedNullFromSprocException : LocalizedException
	{
		// Token: 0x06000215 RID: 533 RVA: 0x0000948C File Offset: 0x0000768C
		public UnexpectedNullFromSprocException(string msg) : base(MigrationMonitorStrings.ErrorUnexpectedNullFromSproc(msg))
		{
			this.msg = msg;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x000094A1 File Offset: 0x000076A1
		public UnexpectedNullFromSprocException(string msg, Exception innerException) : base(MigrationMonitorStrings.ErrorUnexpectedNullFromSproc(msg), innerException)
		{
			this.msg = msg;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x000094B7 File Offset: 0x000076B7
		protected UnexpectedNullFromSprocException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.msg = (string)info.GetValue("msg", typeof(string));
		}

		// Token: 0x06000218 RID: 536 RVA: 0x000094E1 File Offset: 0x000076E1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("msg", this.msg);
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000219 RID: 537 RVA: 0x000094FC File Offset: 0x000076FC
		public string Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x04000162 RID: 354
		private readonly string msg;
	}
}
