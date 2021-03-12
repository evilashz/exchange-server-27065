using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FEE RID: 4078
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DataClassificationNonUniqueNameViolationException : LocalizedException
	{
		// Token: 0x0600AE68 RID: 44648 RVA: 0x00292E48 File Offset: 0x00291048
		public DataClassificationNonUniqueNameViolationException(string name) : base(Strings.DataClassificationNonUniqueNameViolation(name))
		{
			this.name = name;
		}

		// Token: 0x0600AE69 RID: 44649 RVA: 0x00292E5D File Offset: 0x0029105D
		public DataClassificationNonUniqueNameViolationException(string name, Exception innerException) : base(Strings.DataClassificationNonUniqueNameViolation(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x0600AE6A RID: 44650 RVA: 0x00292E73 File Offset: 0x00291073
		protected DataClassificationNonUniqueNameViolationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x0600AE6B RID: 44651 RVA: 0x00292E9D File Offset: 0x0029109D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x170037CD RID: 14285
		// (get) Token: 0x0600AE6C RID: 44652 RVA: 0x00292EB8 File Offset: 0x002910B8
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04006133 RID: 24883
		private readonly string name;
	}
}
