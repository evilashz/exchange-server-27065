using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x02000FB1 RID: 4017
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IisTasksValidationStringLengthOutOfRangeException : LocalizedException
	{
		// Token: 0x0600AD45 RID: 44357 RVA: 0x0029157D File Offset: 0x0028F77D
		public IisTasksValidationStringLengthOutOfRangeException(string propertyName, int minLength, int maxLength) : base(Strings.IisTasksValidationStringLengthOutOfRangeException(propertyName, minLength, maxLength))
		{
			this.propertyName = propertyName;
			this.minLength = minLength;
			this.maxLength = maxLength;
		}

		// Token: 0x0600AD46 RID: 44358 RVA: 0x002915A2 File Offset: 0x0028F7A2
		public IisTasksValidationStringLengthOutOfRangeException(string propertyName, int minLength, int maxLength, Exception innerException) : base(Strings.IisTasksValidationStringLengthOutOfRangeException(propertyName, minLength, maxLength), innerException)
		{
			this.propertyName = propertyName;
			this.minLength = minLength;
			this.maxLength = maxLength;
		}

		// Token: 0x0600AD47 RID: 44359 RVA: 0x002915CC File Offset: 0x0028F7CC
		protected IisTasksValidationStringLengthOutOfRangeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.propertyName = (string)info.GetValue("propertyName", typeof(string));
			this.minLength = (int)info.GetValue("minLength", typeof(int));
			this.maxLength = (int)info.GetValue("maxLength", typeof(int));
		}

		// Token: 0x0600AD48 RID: 44360 RVA: 0x00291641 File Offset: 0x0028F841
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("propertyName", this.propertyName);
			info.AddValue("minLength", this.minLength);
			info.AddValue("maxLength", this.maxLength);
		}

		// Token: 0x1700379E RID: 14238
		// (get) Token: 0x0600AD49 RID: 44361 RVA: 0x0029167E File Offset: 0x0028F87E
		public string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x1700379F RID: 14239
		// (get) Token: 0x0600AD4A RID: 44362 RVA: 0x00291686 File Offset: 0x0028F886
		public int MinLength
		{
			get
			{
				return this.minLength;
			}
		}

		// Token: 0x170037A0 RID: 14240
		// (get) Token: 0x0600AD4B RID: 44363 RVA: 0x0029168E File Offset: 0x0028F88E
		public int MaxLength
		{
			get
			{
				return this.maxLength;
			}
		}

		// Token: 0x04006104 RID: 24836
		private readonly string propertyName;

		// Token: 0x04006105 RID: 24837
		private readonly int minLength;

		// Token: 0x04006106 RID: 24838
		private readonly int maxLength;
	}
}
