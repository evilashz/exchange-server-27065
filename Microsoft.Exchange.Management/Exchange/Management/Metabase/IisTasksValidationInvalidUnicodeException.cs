using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x02000FB2 RID: 4018
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IisTasksValidationInvalidUnicodeException : LocalizedException
	{
		// Token: 0x0600AD4C RID: 44364 RVA: 0x00291696 File Offset: 0x0028F896
		public IisTasksValidationInvalidUnicodeException(string propertyName, string value, char badChar, int unicodeValue, int charIndex) : base(Strings.IisTasksValidationInvalidUnicodeException(propertyName, value, badChar, unicodeValue, charIndex))
		{
			this.propertyName = propertyName;
			this.value = value;
			this.badChar = badChar;
			this.unicodeValue = unicodeValue;
			this.charIndex = charIndex;
		}

		// Token: 0x0600AD4D RID: 44365 RVA: 0x002916CF File Offset: 0x0028F8CF
		public IisTasksValidationInvalidUnicodeException(string propertyName, string value, char badChar, int unicodeValue, int charIndex, Exception innerException) : base(Strings.IisTasksValidationInvalidUnicodeException(propertyName, value, badChar, unicodeValue, charIndex), innerException)
		{
			this.propertyName = propertyName;
			this.value = value;
			this.badChar = badChar;
			this.unicodeValue = unicodeValue;
			this.charIndex = charIndex;
		}

		// Token: 0x0600AD4E RID: 44366 RVA: 0x0029170C File Offset: 0x0028F90C
		protected IisTasksValidationInvalidUnicodeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.propertyName = (string)info.GetValue("propertyName", typeof(string));
			this.value = (string)info.GetValue("value", typeof(string));
			this.badChar = (char)info.GetValue("badChar", typeof(char));
			this.unicodeValue = (int)info.GetValue("unicodeValue", typeof(int));
			this.charIndex = (int)info.GetValue("charIndex", typeof(int));
		}

		// Token: 0x0600AD4F RID: 44367 RVA: 0x002917C4 File Offset: 0x0028F9C4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("propertyName", this.propertyName);
			info.AddValue("value", this.value);
			info.AddValue("badChar", this.badChar);
			info.AddValue("unicodeValue", this.unicodeValue);
			info.AddValue("charIndex", this.charIndex);
		}

		// Token: 0x170037A1 RID: 14241
		// (get) Token: 0x0600AD50 RID: 44368 RVA: 0x0029182E File Offset: 0x0028FA2E
		public string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x170037A2 RID: 14242
		// (get) Token: 0x0600AD51 RID: 44369 RVA: 0x00291836 File Offset: 0x0028FA36
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x170037A3 RID: 14243
		// (get) Token: 0x0600AD52 RID: 44370 RVA: 0x0029183E File Offset: 0x0028FA3E
		public char BadChar
		{
			get
			{
				return this.badChar;
			}
		}

		// Token: 0x170037A4 RID: 14244
		// (get) Token: 0x0600AD53 RID: 44371 RVA: 0x00291846 File Offset: 0x0028FA46
		public int UnicodeValue
		{
			get
			{
				return this.unicodeValue;
			}
		}

		// Token: 0x170037A5 RID: 14245
		// (get) Token: 0x0600AD54 RID: 44372 RVA: 0x0029184E File Offset: 0x0028FA4E
		public int CharIndex
		{
			get
			{
				return this.charIndex;
			}
		}

		// Token: 0x04006107 RID: 24839
		private readonly string propertyName;

		// Token: 0x04006108 RID: 24840
		private readonly string value;

		// Token: 0x04006109 RID: 24841
		private readonly char badChar;

		// Token: 0x0400610A RID: 24842
		private readonly int unicodeValue;

		// Token: 0x0400610B RID: 24843
		private readonly int charIndex;
	}
}
