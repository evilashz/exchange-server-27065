using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FC5 RID: 4037
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExceededMaximumCollectionCountException : LocalizedException
	{
		// Token: 0x0600ADAF RID: 44463 RVA: 0x002920FC File Offset: 0x002902FC
		public ExceededMaximumCollectionCountException(string propertyName, int maxLength, int actualLength) : base(Strings.ErrorExceededMaximumCollectionCount(propertyName, maxLength, actualLength))
		{
			this.propertyName = propertyName;
			this.maxLength = maxLength;
			this.actualLength = actualLength;
		}

		// Token: 0x0600ADB0 RID: 44464 RVA: 0x00292121 File Offset: 0x00290321
		public ExceededMaximumCollectionCountException(string propertyName, int maxLength, int actualLength, Exception innerException) : base(Strings.ErrorExceededMaximumCollectionCount(propertyName, maxLength, actualLength), innerException)
		{
			this.propertyName = propertyName;
			this.maxLength = maxLength;
			this.actualLength = actualLength;
		}

		// Token: 0x0600ADB1 RID: 44465 RVA: 0x00292148 File Offset: 0x00290348
		protected ExceededMaximumCollectionCountException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.propertyName = (string)info.GetValue("propertyName", typeof(string));
			this.maxLength = (int)info.GetValue("maxLength", typeof(int));
			this.actualLength = (int)info.GetValue("actualLength", typeof(int));
		}

		// Token: 0x0600ADB2 RID: 44466 RVA: 0x002921BD File Offset: 0x002903BD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("propertyName", this.propertyName);
			info.AddValue("maxLength", this.maxLength);
			info.AddValue("actualLength", this.actualLength);
		}

		// Token: 0x170037B8 RID: 14264
		// (get) Token: 0x0600ADB3 RID: 44467 RVA: 0x002921FA File Offset: 0x002903FA
		public string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x170037B9 RID: 14265
		// (get) Token: 0x0600ADB4 RID: 44468 RVA: 0x00292202 File Offset: 0x00290402
		public int MaxLength
		{
			get
			{
				return this.maxLength;
			}
		}

		// Token: 0x170037BA RID: 14266
		// (get) Token: 0x0600ADB5 RID: 44469 RVA: 0x0029220A File Offset: 0x0029040A
		public int ActualLength
		{
			get
			{
				return this.actualLength;
			}
		}

		// Token: 0x0400611E RID: 24862
		private readonly string propertyName;

		// Token: 0x0400611F RID: 24863
		private readonly int maxLength;

		// Token: 0x04006120 RID: 24864
		private readonly int actualLength;
	}
}
