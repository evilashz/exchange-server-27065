using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Mapi.Common
{
	// Token: 0x02000043 RID: 67
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MapiExtractingException : MapiConvertingException
	{
		// Token: 0x06000269 RID: 617 RVA: 0x0000DC0C File Offset: 0x0000BE0C
		public MapiExtractingException(string name, string propTag, string propType, string rawValue, string rawValueType, string type, string isMultiValued, string details) : base(Strings.MapiExtractingExceptionError(name, propTag, propType, rawValue, rawValueType, type, isMultiValued, details))
		{
			this.name = name;
			this.propTag = propTag;
			this.propType = propType;
			this.rawValue = rawValue;
			this.rawValueType = rawValueType;
			this.type = type;
			this.isMultiValued = isMultiValued;
			this.details = details;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000DC70 File Offset: 0x0000BE70
		public MapiExtractingException(string name, string propTag, string propType, string rawValue, string rawValueType, string type, string isMultiValued, string details, Exception innerException) : base(Strings.MapiExtractingExceptionError(name, propTag, propType, rawValue, rawValueType, type, isMultiValued, details), innerException)
		{
			this.name = name;
			this.propTag = propTag;
			this.propType = propType;
			this.rawValue = rawValue;
			this.rawValueType = rawValueType;
			this.type = type;
			this.isMultiValued = isMultiValued;
			this.details = details;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000DCD4 File Offset: 0x0000BED4
		protected MapiExtractingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
			this.propTag = (string)info.GetValue("propTag", typeof(string));
			this.propType = (string)info.GetValue("propType", typeof(string));
			this.rawValue = (string)info.GetValue("rawValue", typeof(string));
			this.rawValueType = (string)info.GetValue("rawValueType", typeof(string));
			this.type = (string)info.GetValue("type", typeof(string));
			this.isMultiValued = (string)info.GetValue("isMultiValued", typeof(string));
			this.details = (string)info.GetValue("details", typeof(string));
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000DDEC File Offset: 0x0000BFEC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
			info.AddValue("propTag", this.propTag);
			info.AddValue("propType", this.propType);
			info.AddValue("rawValue", this.rawValue);
			info.AddValue("rawValueType", this.rawValueType);
			info.AddValue("type", this.type);
			info.AddValue("isMultiValued", this.isMultiValued);
			info.AddValue("details", this.details);
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000DE89 File Offset: 0x0000C089
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000DE91 File Offset: 0x0000C091
		public string PropTag
		{
			get
			{
				return this.propTag;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600026F RID: 623 RVA: 0x0000DE99 File Offset: 0x0000C099
		public string PropType
		{
			get
			{
				return this.propType;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0000DEA1 File Offset: 0x0000C0A1
		public string RawValue
		{
			get
			{
				return this.rawValue;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000271 RID: 625 RVA: 0x0000DEA9 File Offset: 0x0000C0A9
		public string RawValueType
		{
			get
			{
				return this.rawValueType;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0000DEB1 File Offset: 0x0000C0B1
		public string Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000DEB9 File Offset: 0x0000C0B9
		public string IsMultiValued
		{
			get
			{
				return this.isMultiValued;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000DEC1 File Offset: 0x0000C0C1
		public string Details
		{
			get
			{
				return this.details;
			}
		}

		// Token: 0x0400018A RID: 394
		private readonly string name;

		// Token: 0x0400018B RID: 395
		private readonly string propTag;

		// Token: 0x0400018C RID: 396
		private readonly string propType;

		// Token: 0x0400018D RID: 397
		private readonly string rawValue;

		// Token: 0x0400018E RID: 398
		private readonly string rawValueType;

		// Token: 0x0400018F RID: 399
		private readonly string type;

		// Token: 0x04000190 RID: 400
		private readonly string isMultiValued;

		// Token: 0x04000191 RID: 401
		private readonly string details;
	}
}
