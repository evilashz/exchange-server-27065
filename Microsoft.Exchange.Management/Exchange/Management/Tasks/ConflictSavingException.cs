using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FBE RID: 4030
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ConflictSavingException : LocalizedException
	{
		// Token: 0x0600AD8C RID: 44428 RVA: 0x00291D9E File Offset: 0x0028FF9E
		public ConflictSavingException(string identiy) : base(Strings.ErrorConflictSaving(identiy))
		{
			this.identiy = identiy;
		}

		// Token: 0x0600AD8D RID: 44429 RVA: 0x00291DB3 File Offset: 0x0028FFB3
		public ConflictSavingException(string identiy, Exception innerException) : base(Strings.ErrorConflictSaving(identiy), innerException)
		{
			this.identiy = identiy;
		}

		// Token: 0x0600AD8E RID: 44430 RVA: 0x00291DC9 File Offset: 0x0028FFC9
		protected ConflictSavingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.identiy = (string)info.GetValue("identiy", typeof(string));
		}

		// Token: 0x0600AD8F RID: 44431 RVA: 0x00291DF3 File Offset: 0x0028FFF3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("identiy", this.identiy);
		}

		// Token: 0x170037B1 RID: 14257
		// (get) Token: 0x0600AD90 RID: 44432 RVA: 0x00291E0E File Offset: 0x0029000E
		public string Identiy
		{
			get
			{
				return this.identiy;
			}
		}

		// Token: 0x04006117 RID: 24855
		private readonly string identiy;
	}
}
