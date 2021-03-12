using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020010D4 RID: 4308
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class EtrHasRmsActionException : LocalizedException
	{
		// Token: 0x0600B315 RID: 45845 RVA: 0x0029AB66 File Offset: 0x00298D66
		public EtrHasRmsActionException(string ruleName) : base(Strings.EtrHasRmsAction(ruleName))
		{
			this.ruleName = ruleName;
		}

		// Token: 0x0600B316 RID: 45846 RVA: 0x0029AB7B File Offset: 0x00298D7B
		public EtrHasRmsActionException(string ruleName, Exception innerException) : base(Strings.EtrHasRmsAction(ruleName), innerException)
		{
			this.ruleName = ruleName;
		}

		// Token: 0x0600B317 RID: 45847 RVA: 0x0029AB91 File Offset: 0x00298D91
		protected EtrHasRmsActionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ruleName = (string)info.GetValue("ruleName", typeof(string));
		}

		// Token: 0x0600B318 RID: 45848 RVA: 0x0029ABBB File Offset: 0x00298DBB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ruleName", this.ruleName);
		}

		// Token: 0x170038E2 RID: 14562
		// (get) Token: 0x0600B319 RID: 45849 RVA: 0x0029ABD6 File Offset: 0x00298DD6
		public string RuleName
		{
			get
			{
				return this.ruleName;
			}
		}

		// Token: 0x04006248 RID: 25160
		private readonly string ruleName;
	}
}
