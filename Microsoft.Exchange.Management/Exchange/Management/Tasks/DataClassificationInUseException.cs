using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FEF RID: 4079
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DataClassificationInUseException : LocalizedException
	{
		// Token: 0x0600AE6D RID: 44653 RVA: 0x00292EC0 File Offset: 0x002910C0
		public DataClassificationInUseException(string identity, string rules) : base(Strings.ErrorDataClassificationIsInUse(identity, rules))
		{
			this.identity = identity;
			this.rules = rules;
		}

		// Token: 0x0600AE6E RID: 44654 RVA: 0x00292EDD File Offset: 0x002910DD
		public DataClassificationInUseException(string identity, string rules, Exception innerException) : base(Strings.ErrorDataClassificationIsInUse(identity, rules), innerException)
		{
			this.identity = identity;
			this.rules = rules;
		}

		// Token: 0x0600AE6F RID: 44655 RVA: 0x00292EFC File Offset: 0x002910FC
		protected DataClassificationInUseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.identity = (string)info.GetValue("identity", typeof(string));
			this.rules = (string)info.GetValue("rules", typeof(string));
		}

		// Token: 0x0600AE70 RID: 44656 RVA: 0x00292F51 File Offset: 0x00291151
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("identity", this.identity);
			info.AddValue("rules", this.rules);
		}

		// Token: 0x170037CE RID: 14286
		// (get) Token: 0x0600AE71 RID: 44657 RVA: 0x00292F7D File Offset: 0x0029117D
		public string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x170037CF RID: 14287
		// (get) Token: 0x0600AE72 RID: 44658 RVA: 0x00292F85 File Offset: 0x00291185
		public string Rules
		{
			get
			{
				return this.rules;
			}
		}

		// Token: 0x04006134 RID: 24884
		private readonly string identity;

		// Token: 0x04006135 RID: 24885
		private readonly string rules;
	}
}
