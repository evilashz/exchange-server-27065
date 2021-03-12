using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FEB RID: 4075
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DataClassificationFingerprintsMissingException : LocalizedException
	{
		// Token: 0x0600AE59 RID: 44633 RVA: 0x00292CE0 File Offset: 0x00290EE0
		public DataClassificationFingerprintsMissingException(string name) : base(Strings.DataClassificationFingerprintsMissing(name))
		{
			this.name = name;
		}

		// Token: 0x0600AE5A RID: 44634 RVA: 0x00292CF5 File Offset: 0x00290EF5
		public DataClassificationFingerprintsMissingException(string name, Exception innerException) : base(Strings.DataClassificationFingerprintsMissing(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x0600AE5B RID: 44635 RVA: 0x00292D0B File Offset: 0x00290F0B
		protected DataClassificationFingerprintsMissingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x0600AE5C RID: 44636 RVA: 0x00292D35 File Offset: 0x00290F35
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x170037CA RID: 14282
		// (get) Token: 0x0600AE5D RID: 44637 RVA: 0x00292D50 File Offset: 0x00290F50
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04006130 RID: 24880
		private readonly string name;
	}
}
