using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Mapi.Common
{
	// Token: 0x02000044 RID: 68
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MapiPackingException : MapiConvertingException
	{
		// Token: 0x06000275 RID: 629 RVA: 0x0000DECC File Offset: 0x0000C0CC
		public MapiPackingException(string name, string value, string type, string isMultiValued, string propTag, string propType, string details) : base(Strings.MapiPackingExceptionError(name, value, type, isMultiValued, propTag, propType, details))
		{
			this.name = name;
			this.value = value;
			this.type = type;
			this.isMultiValued = isMultiValued;
			this.propTag = propTag;
			this.propType = propType;
			this.details = details;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000DF24 File Offset: 0x0000C124
		public MapiPackingException(string name, string value, string type, string isMultiValued, string propTag, string propType, string details, Exception innerException) : base(Strings.MapiPackingExceptionError(name, value, type, isMultiValued, propTag, propType, details), innerException)
		{
			this.name = name;
			this.value = value;
			this.type = type;
			this.isMultiValued = isMultiValued;
			this.propTag = propTag;
			this.propType = propType;
			this.details = details;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000DF80 File Offset: 0x0000C180
		protected MapiPackingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
			this.value = (string)info.GetValue("value", typeof(string));
			this.type = (string)info.GetValue("type", typeof(string));
			this.isMultiValued = (string)info.GetValue("isMultiValued", typeof(string));
			this.propTag = (string)info.GetValue("propTag", typeof(string));
			this.propType = (string)info.GetValue("propType", typeof(string));
			this.details = (string)info.GetValue("details", typeof(string));
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000E078 File Offset: 0x0000C278
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
			info.AddValue("value", this.value);
			info.AddValue("type", this.type);
			info.AddValue("isMultiValued", this.isMultiValued);
			info.AddValue("propTag", this.propTag);
			info.AddValue("propType", this.propType);
			info.AddValue("details", this.details);
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0000E104 File Offset: 0x0000C304
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600027A RID: 634 RVA: 0x0000E10C File Offset: 0x0000C30C
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0000E114 File Offset: 0x0000C314
		public string Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600027C RID: 636 RVA: 0x0000E11C File Offset: 0x0000C31C
		public string IsMultiValued
		{
			get
			{
				return this.isMultiValued;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0000E124 File Offset: 0x0000C324
		public string PropTag
		{
			get
			{
				return this.propTag;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600027E RID: 638 RVA: 0x0000E12C File Offset: 0x0000C32C
		public string PropType
		{
			get
			{
				return this.propType;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000E134 File Offset: 0x0000C334
		public string Details
		{
			get
			{
				return this.details;
			}
		}

		// Token: 0x04000192 RID: 402
		private readonly string name;

		// Token: 0x04000193 RID: 403
		private readonly string value;

		// Token: 0x04000194 RID: 404
		private readonly string type;

		// Token: 0x04000195 RID: 405
		private readonly string isMultiValued;

		// Token: 0x04000196 RID: 406
		private readonly string propTag;

		// Token: 0x04000197 RID: 407
		private readonly string propType;

		// Token: 0x04000198 RID: 408
		private readonly string details;
	}
}
