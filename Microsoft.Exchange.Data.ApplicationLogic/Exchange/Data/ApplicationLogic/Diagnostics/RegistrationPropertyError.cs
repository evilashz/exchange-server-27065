using System;

namespace Microsoft.Exchange.Data.ApplicationLogic.Diagnostics
{
	// Token: 0x020000E0 RID: 224
	internal class RegistrationPropertyError
	{
		// Token: 0x06000973 RID: 2419 RVA: 0x000254BA File Offset: 0x000236BA
		public RegistrationPropertyError(PropertyDefinition propDef, PropertyErrorCode errorCode)
		{
			this.PropertyDefinition = propDef;
			this.ErrorCode = errorCode;
			this.Exception = null;
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x000254D7 File Offset: 0x000236D7
		public RegistrationPropertyError(PropertyDefinition propDef, Exception exception)
		{
			this.PropertyDefinition = propDef;
			this.ErrorCode = PropertyErrorCode.Exception;
			this.Exception = exception;
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x000254F4 File Offset: 0x000236F4
		// (set) Token: 0x06000976 RID: 2422 RVA: 0x000254FC File Offset: 0x000236FC
		public PropertyErrorCode ErrorCode { get; private set; }

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000977 RID: 2423 RVA: 0x00025505 File Offset: 0x00023705
		// (set) Token: 0x06000978 RID: 2424 RVA: 0x0002550D File Offset: 0x0002370D
		public PropertyDefinition PropertyDefinition { get; private set; }

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000979 RID: 2425 RVA: 0x00025516 File Offset: 0x00023716
		// (set) Token: 0x0600097A RID: 2426 RVA: 0x0002551E File Offset: 0x0002371E
		public Exception Exception { get; private set; }

		// Token: 0x0600097B RID: 2427 RVA: 0x00025528 File Offset: 0x00023728
		public override string ToString()
		{
			return string.Format("[PropertyError] Property: {0}[{1}], Code: {2}, Exception: {3}", new object[]
			{
				this.PropertyDefinition.Name,
				this.PropertyDefinition.Type.Name,
				this.ErrorCode,
				(this.Exception == null) ? "NULL" : this.Exception.ToString()
			});
		}
	}
}
