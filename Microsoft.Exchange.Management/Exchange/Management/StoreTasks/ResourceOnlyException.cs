using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x02000FB6 RID: 4022
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ResourceOnlyException : LocalizedException
	{
		// Token: 0x0600AD67 RID: 44391 RVA: 0x00291AB9 File Offset: 0x0028FCB9
		public ResourceOnlyException(string parm) : base(Strings.ResourceOnly(parm))
		{
			this.parm = parm;
		}

		// Token: 0x0600AD68 RID: 44392 RVA: 0x00291ACE File Offset: 0x0028FCCE
		public ResourceOnlyException(string parm, Exception innerException) : base(Strings.ResourceOnly(parm), innerException)
		{
			this.parm = parm;
		}

		// Token: 0x0600AD69 RID: 44393 RVA: 0x00291AE4 File Offset: 0x0028FCE4
		protected ResourceOnlyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.parm = (string)info.GetValue("parm", typeof(string));
		}

		// Token: 0x0600AD6A RID: 44394 RVA: 0x00291B0E File Offset: 0x0028FD0E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("parm", this.parm);
		}

		// Token: 0x170037AC RID: 14252
		// (get) Token: 0x0600AD6B RID: 44395 RVA: 0x00291B29 File Offset: 0x0028FD29
		public string Parm
		{
			get
			{
				return this.parm;
			}
		}

		// Token: 0x04006112 RID: 24850
		private readonly string parm;
	}
}
