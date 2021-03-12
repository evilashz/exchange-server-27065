using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FED RID: 4077
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DataClassificationFingerprintsDuplicatedException : LocalizedException
	{
		// Token: 0x0600AE63 RID: 44643 RVA: 0x00292DD0 File Offset: 0x00290FD0
		public DataClassificationFingerprintsDuplicatedException(string name) : base(Strings.DataClassificationFingerprintsDuplicated(name))
		{
			this.name = name;
		}

		// Token: 0x0600AE64 RID: 44644 RVA: 0x00292DE5 File Offset: 0x00290FE5
		public DataClassificationFingerprintsDuplicatedException(string name, Exception innerException) : base(Strings.DataClassificationFingerprintsDuplicated(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x0600AE65 RID: 44645 RVA: 0x00292DFB File Offset: 0x00290FFB
		protected DataClassificationFingerprintsDuplicatedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x0600AE66 RID: 44646 RVA: 0x00292E25 File Offset: 0x00291025
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x170037CC RID: 14284
		// (get) Token: 0x0600AE67 RID: 44647 RVA: 0x00292E40 File Offset: 0x00291040
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04006132 RID: 24882
		private readonly string name;
	}
}
