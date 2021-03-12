using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FEC RID: 4076
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DataClassificationFingerprintsDescriptionMissingException : LocalizedException
	{
		// Token: 0x0600AE5E RID: 44638 RVA: 0x00292D58 File Offset: 0x00290F58
		public DataClassificationFingerprintsDescriptionMissingException(string name) : base(Strings.DataClassificationFingerprintsDescriptionMissing(name))
		{
			this.name = name;
		}

		// Token: 0x0600AE5F RID: 44639 RVA: 0x00292D6D File Offset: 0x00290F6D
		public DataClassificationFingerprintsDescriptionMissingException(string name, Exception innerException) : base(Strings.DataClassificationFingerprintsDescriptionMissing(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x0600AE60 RID: 44640 RVA: 0x00292D83 File Offset: 0x00290F83
		protected DataClassificationFingerprintsDescriptionMissingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x0600AE61 RID: 44641 RVA: 0x00292DAD File Offset: 0x00290FAD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x170037CB RID: 14283
		// (get) Token: 0x0600AE62 RID: 44642 RVA: 0x00292DC8 File Offset: 0x00290FC8
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04006131 RID: 24881
		private readonly string name;
	}
}
