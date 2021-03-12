using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000021 RID: 33
	[Serializable]
	public class PropertyConversionError : PropertyValidationError
	{
		// Token: 0x06000120 RID: 288 RVA: 0x00005558 File Offset: 0x00003758
		public PropertyConversionError(LocalizedString description, PropertyDefinition propertyDefinition, object invalidData, Exception exception) : base(description, propertyDefinition, invalidData)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			this.exception = exception;
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000121 RID: 289 RVA: 0x0000557A File Offset: 0x0000377A
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00005582 File Offset: 0x00003782
		public bool Equals(PropertyConversionError other)
		{
			return other != null && object.Equals(this.Exception, other.Exception) && base.Equals(other);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000055A5 File Offset: 0x000037A5
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PropertyConversionError);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000055B3 File Offset: 0x000037B3
		public override int GetHashCode()
		{
			return base.GetHashCode() ^ this.Exception.GetHashCode();
		}

		// Token: 0x0400005F RID: 95
		private Exception exception;
	}
}
