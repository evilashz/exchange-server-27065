using System;
using System.Management.Automation;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200006B RID: 107
	public class MailboxProvisioningConstraint : XMLSerializableBase, IMailboxProvisioningConstraint
	{
		// Token: 0x060004E5 RID: 1253 RVA: 0x0001C279 File Offset: 0x0001A479
		public MailboxProvisioningConstraint()
		{
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0001C281 File Offset: 0x0001A481
		public MailboxProvisioningConstraint(string value)
		{
			this.Value = value;
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x0001C290 File Offset: 0x0001A490
		// (set) Token: 0x060004E8 RID: 1256 RVA: 0x0001C2A1 File Offset: 0x0001A4A1
		[XmlText]
		public string Value
		{
			get
			{
				return this.value ?? string.Empty;
			}
			set
			{
				this.value = value;
				this.filter = null;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x0001C2B1 File Offset: 0x0001A4B1
		[XmlIgnore]
		public bool IsEmpty
		{
			get
			{
				return string.IsNullOrEmpty(this.value);
			}
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0001C2C0 File Offset: 0x0001A4C0
		public static object ConvertValueFromString(object valueToConvert, Type resultType)
		{
			string text = valueToConvert as string;
			bool flag;
			if (resultType == typeof(bool) && bool.TryParse(text, out flag))
			{
				return flag;
			}
			if (resultType.IsGenericType && resultType.GetGenericTypeDefinition() == typeof(Nullable<>))
			{
				bool flag2 = text == null || "null".Equals(text, StringComparison.OrdinalIgnoreCase) || "$null".Equals(text, StringComparison.OrdinalIgnoreCase);
				if (flag2)
				{
					return null;
				}
			}
			return LanguagePrimitives.ConvertTo(text, resultType);
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0001C344 File Offset: 0x0001A544
		public bool IsMatch(MailboxProvisioningAttributes attributes)
		{
			if (!string.IsNullOrEmpty(this.Value) && this.filter == null)
			{
				this.filter = this.ParseFilter();
			}
			return string.IsNullOrEmpty(this.Value) || OpathFilterEvaluator.FilterMatches(this.filter, attributes.PropertyBag);
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0001C392 File Offset: 0x0001A592
		public override string ToString()
		{
			return this.Value;
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0001C39A File Offset: 0x0001A59A
		public override bool Equals(object obj)
		{
			return !object.ReferenceEquals(null, obj) && (object.ReferenceEquals(this, obj) || (obj is MailboxProvisioningConstraint && this.Equals((MailboxProvisioningConstraint)obj)));
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0001C3C8 File Offset: 0x0001A5C8
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0001C3D8 File Offset: 0x0001A5D8
		public bool TryValidate(out InvalidMailboxProvisioningConstraintException validationException)
		{
			validationException = null;
			try
			{
				this.ParseFilter();
			}
			catch (InvalidMailboxProvisioningConstraintException ex)
			{
				validationException = ex;
			}
			return validationException == null;
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0001C40C File Offset: 0x0001A60C
		private QueryFilter ParseFilter()
		{
			ObjectSchema instance = ObjectSchema.GetInstance<MailboxProvisioningAttributesSchema>();
			Exception ex = null;
			QueryFilter result = null;
			try
			{
				QueryParser queryParser = new QueryParser(this.Value, instance, QueryParser.Capabilities.All, null, new QueryParser.ConvertValueFromStringDelegate(MailboxProvisioningConstraint.ConvertValueFromString));
				result = queryParser.ParseTree;
			}
			catch (ParsingNonFilterablePropertyException ex2)
			{
				ex = ex2;
			}
			catch (ParsingException ex3)
			{
				ex = ex3;
			}
			catch (ArgumentOutOfRangeException ex4)
			{
				ex = ex4;
			}
			if (ex != null)
			{
				throw new InvalidMailboxProvisioningConstraintException(ex.Message, ex);
			}
			return result;
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0001C49C File Offset: 0x0001A69C
		private bool Equals(MailboxProvisioningConstraint other)
		{
			return string.Equals(this.Value, other.Value);
		}

		// Token: 0x0400021B RID: 539
		private string value;

		// Token: 0x0400021C RID: 540
		private QueryFilter filter;
	}
}
