using System;
using System.Collections.Generic;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000412 RID: 1042
	[Serializable]
	public class SystemMessage : ADConfigurationObject
	{
		// Token: 0x17000D68 RID: 3432
		// (get) Token: 0x06002F1C RID: 12060 RVA: 0x000BE810 File Offset: 0x000BCA10
		internal override ADObjectSchema Schema
		{
			get
			{
				return SystemMessage.schema;
			}
		}

		// Token: 0x17000D69 RID: 3433
		// (get) Token: 0x06002F1D RID: 12061 RVA: 0x000BE817 File Offset: 0x000BCA17
		// (set) Token: 0x06002F1E RID: 12062 RVA: 0x000BE829 File Offset: 0x000BCA29
		[Parameter(Mandatory = false)]
		public string Text
		{
			get
			{
				return (string)this[SystemMessageSchema.Text];
			}
			set
			{
				this[SystemMessageSchema.Text] = value;
			}
		}

		// Token: 0x17000D6A RID: 3434
		// (get) Token: 0x06002F1F RID: 12063 RVA: 0x000BE837 File Offset: 0x000BCA37
		public bool Internal
		{
			get
			{
				return (bool)this[SystemMessageSchema.Internal];
			}
		}

		// Token: 0x17000D6B RID: 3435
		// (get) Token: 0x06002F20 RID: 12064 RVA: 0x000BE849 File Offset: 0x000BCA49
		public CultureInfo Language
		{
			get
			{
				return (CultureInfo)this[SystemMessageSchema.Language];
			}
		}

		// Token: 0x17000D6C RID: 3436
		// (get) Token: 0x06002F21 RID: 12065 RVA: 0x000BE85B File Offset: 0x000BCA5B
		public EnhancedStatusCode DsnCode
		{
			get
			{
				return (EnhancedStatusCode)this[SystemMessageSchema.DsnCode];
			}
		}

		// Token: 0x17000D6D RID: 3437
		// (get) Token: 0x06002F22 RID: 12066 RVA: 0x000BE86D File Offset: 0x000BCA6D
		public QuotaMessageType? QuotaMessageType
		{
			get
			{
				return (QuotaMessageType?)this[SystemMessageSchema.QuotaMessageType];
			}
		}

		// Token: 0x17000D6E RID: 3438
		// (get) Token: 0x06002F23 RID: 12067 RVA: 0x000BE87F File Offset: 0x000BCA7F
		internal override string MostDerivedObjectClass
		{
			get
			{
				return SystemMessage.mostDerivedClass;
			}
		}

		// Token: 0x06002F24 RID: 12068 RVA: 0x000BE888 File Offset: 0x000BCA88
		internal static ADObjectId GetDsnCustomizationContainer(ADObjectId orgContainer)
		{
			ADObjectId childId = orgContainer.GetChildId("Transport Settings");
			return childId.GetChildId("Dsn Customization");
		}

		// Token: 0x06002F25 RID: 12069 RVA: 0x000BE8AC File Offset: 0x000BCAAC
		internal static object LanguageGetter(IPropertyBag propertyBag)
		{
			object result;
			try
			{
				string escapedName = ((ADObjectId)propertyBag[ADObjectSchema.Id]).AncestorDN(1).Rdn.EscapedName;
				int culture;
				if (int.TryParse(escapedName, NumberStyles.None, NumberFormatInfo.InvariantInfo, out culture))
				{
					result = CultureInfo.GetCultureInfo(culture);
				}
				else
				{
					escapedName = ((ADObjectId)propertyBag[ADObjectSchema.Id]).AncestorDN(2).Rdn.EscapedName;
					if (int.TryParse(escapedName, NumberStyles.None, NumberFormatInfo.InvariantInfo, out culture))
					{
						result = CultureInfo.GetCultureInfo(culture);
					}
					else
					{
						result = null;
					}
				}
			}
			catch (InvalidOperationException ex)
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty("Language", ex.Message), SystemMessageSchema.Language, propertyBag[ADObjectSchema.Id]), ex);
			}
			return result;
		}

		// Token: 0x06002F26 RID: 12070 RVA: 0x000BE970 File Offset: 0x000BCB70
		internal static object CodeGetter(IPropertyBag propertyBag)
		{
			string escapedName = ((ADObjectId)propertyBag[ADObjectSchema.Id]).Rdn.EscapedName;
			EnhancedStatusCode result;
			if (EnhancedStatusCode.TryParse(escapedName, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06002F27 RID: 12071 RVA: 0x000BE9A8 File Offset: 0x000BCBA8
		internal static object InternalGetter(IPropertyBag propertyBag)
		{
			object result;
			try
			{
				ADObjectId adobjectId = (ADObjectId)propertyBag[ADObjectSchema.Id];
				if (adobjectId == null)
				{
					throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty("Internal", DirectoryStrings.CannotGetDnAtDepth(null, 0)), SystemMessageSchema.Internal, propertyBag[ADObjectSchema.Id]));
				}
				string escapedName = adobjectId.AncestorDN(1).Rdn.EscapedName;
				result = !escapedName.Equals("external", StringComparison.OrdinalIgnoreCase);
			}
			catch (InvalidOperationException ex)
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty("Internal", ex.Message), SystemMessageSchema.Internal, propertyBag[ADObjectSchema.Id]), ex);
			}
			return result;
		}

		// Token: 0x06002F28 RID: 12072 RVA: 0x000BEA64 File Offset: 0x000BCC64
		internal static object QuotaMessageTypeGetter(IPropertyBag propertyBag)
		{
			string escapedName = ((ADObjectId)propertyBag[ADObjectSchema.Id]).Rdn.EscapedName;
			EnhancedStatusCode enhancedStatusCode;
			if (EnhancedStatusCode.TryParse(escapedName, out enhancedStatusCode))
			{
				return null;
			}
			try
			{
				return Enum.Parse(typeof(QuotaMessageType), escapedName, true);
			}
			catch (ArgumentException)
			{
			}
			return null;
		}

		// Token: 0x06002F29 RID: 12073 RVA: 0x000BEAC4 File Offset: 0x000BCCC4
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			if (!ClientCultures.IsCultureSupportedForDsnCustomization(this.Language))
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.DsnLanguageNotSupportedForCustomization, SystemMessageSchema.Language, this));
			}
		}

		// Token: 0x04001FAC RID: 8108
		public const string InternalString = "internal";

		// Token: 0x04001FAD RID: 8109
		public const string ExternalString = "external";

		// Token: 0x04001FAE RID: 8110
		private static SystemMessageSchema schema = ObjectSchema.GetInstance<SystemMessageSchema>();

		// Token: 0x04001FAF RID: 8111
		private static string mostDerivedClass = "msExchDSNMessage";
	}
}
