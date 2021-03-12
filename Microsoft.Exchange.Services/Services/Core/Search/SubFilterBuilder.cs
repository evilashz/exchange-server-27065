﻿using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.Search
{
	// Token: 0x02000281 RID: 641
	internal static class SubFilterBuilder
	{
		// Token: 0x060010B5 RID: 4277 RVA: 0x00050BDC File Offset: 0x0004EDDC
		static SubFilterBuilder()
		{
			SubFilterBuilder.subFilterMap.Add("message:ToRecipients", SubFilterType.RecipientTo);
			SubFilterBuilder.subFilterMap.Add("message:CcRecipients", SubFilterType.RecipientCc);
			SubFilterBuilder.subFilterMap.Add("message:BccRecipients", SubFilterType.RecipientBcc);
			SubFilterBuilder.subFilterMap.Add("calendar:RequiredAttendees", SubFilterType.AttendeeRequired);
			SubFilterBuilder.subFilterMap.Add("calendar:OptionalAttendees", SubFilterType.AttendeeOptional);
			SubFilterBuilder.subFilterMap.Add("calendar:Resources", SubFilterType.AttendeeResource);
			SubFilterBuilder.subFilterMap.Add("item:Attachments", SubFilterType.Attachment);
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x00050C64 File Offset: 0x0004EE64
		public static SubFilterType GetSubFilterForOperand(PropertyPath operandElement)
		{
			if (!(operandElement is PropertyUri))
			{
				return SubFilterType.None;
			}
			string uriString = (operandElement as PropertyUri).UriString;
			SubFilterType result = SubFilterType.None;
			SubFilterBuilder.subFilterMap.TryGetValue(uriString, out result);
			return result;
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x00050C98 File Offset: 0x0004EE98
		public static SubFilterType ValidateExpressionForSubFilter(ContainsExpressionType filterElement)
		{
			return SubFilterBuilder.GetSubFilterForOperand(filterElement.Item);
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x00050CA8 File Offset: 0x0004EEA8
		public static QueryFilter BuildSubFilter(ContainsExpressionType containsExpression, SubFilterType subFilterType)
		{
			QueryFilter result = null;
			if (subFilterType == SubFilterType.None)
			{
				throw new InvalidRestrictionException((CoreResources.IDs)3880436217U);
			}
			string value = containsExpression.Constant.Value;
			MatchOptions matchOptions = SubFilterBuilder.ExtractMatchOptions(containsExpression);
			MatchFlags matchFlags = SubFilterBuilder.ExtractMatchFlags(containsExpression);
			switch (subFilterType)
			{
			case SubFilterType.RecipientTo:
			case SubFilterType.RecipientCc:
			case SubFilterType.RecipientBcc:
			{
				RecipientItemType recipientAttendeeTypeValue = SubFilterBuilder.RecipientItemTypeFromSubFilterType(subFilterType);
				result = SubFilterBuilder.BuildRecipientAttendeeSubFilter(value, matchOptions, matchFlags, RecipientSchema.RecipientType, (int)recipientAttendeeTypeValue);
				break;
			}
			case SubFilterType.AttendeeRequired:
			case SubFilterType.AttendeeOptional:
			case SubFilterType.AttendeeResource:
			{
				AttendeeType recipientAttendeeTypeValue2 = SubFilterBuilder.AttendeeTypeFromSubFilterType(subFilterType);
				result = SubFilterBuilder.BuildRecipientAttendeeSubFilter(value, matchOptions, matchFlags, AttendeeSchema.AttendeeType, (int)recipientAttendeeTypeValue2);
				break;
			}
			case SubFilterType.Attachment:
			{
				TextFilter filter = new TextFilter(AttachmentSchema.DisplayName, value, matchOptions, matchFlags);
				result = new SubFilter(SubFilterProperties.Attachments, filter);
				break;
			}
			}
			return result;
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x00050D5C File Offset: 0x0004EF5C
		private static QueryFilter BuildRecipientAttendeeSubFilter(string emailAddress, MatchOptions matchOptions, MatchFlags matchFlags, PropertyDefinition recipientAttendeeTypePropertyDefinition, int recipientAttendeeTypeValue)
		{
			TextFilter textFilter = new TextFilter(RecipientSchema.EmailAddress, emailAddress, matchOptions, matchFlags);
			TextFilter textFilter2 = new TextFilter(RecipientSchema.SmtpAddress, emailAddress, matchOptions, matchFlags);
			OrFilter orFilter = new OrFilter(new QueryFilter[]
			{
				textFilter,
				textFilter2
			});
			ComparisonFilter comparisonFilter = new ComparisonFilter(ComparisonOperator.Equal, recipientAttendeeTypePropertyDefinition, recipientAttendeeTypeValue);
			AndFilter filter = new AndFilter(new QueryFilter[]
			{
				orFilter,
				comparisonFilter
			});
			return new SubFilter(SubFilterProperties.Recipients, filter);
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x00050DD4 File Offset: 0x0004EFD4
		public static string ExtractMatchOptionsText(TextFilter textFilter)
		{
			switch (textFilter.MatchOptions)
			{
			case MatchOptions.FullString:
				return "FullString";
			case MatchOptions.SubString:
				return "Substring";
			case MatchOptions.Prefix:
				return "Prefixed";
			case MatchOptions.Suffix:
				return "Suffixed";
			case MatchOptions.PrefixOnWords:
				return "PrefixOnWords";
			case MatchOptions.ExactPhrase:
				return "ExactPhrase";
			default:
				return null;
			}
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x00050E30 File Offset: 0x0004F030
		public static string ExtractMatchFlagsText(TextFilter textFilter)
		{
			switch (textFilter.MatchFlags)
			{
			case MatchFlags.Default:
				return "Exact";
			case MatchFlags.IgnoreCase:
				return "IgnoreCase";
			case MatchFlags.IgnoreNonSpace:
				return "IgnoreNonSpacingCharacters";
			case MatchFlags.IgnoreCase | MatchFlags.IgnoreNonSpace:
				return "IgnoreCaseAndNonSpacingCharacters";
			case MatchFlags.Loose:
				return "Loose";
			case MatchFlags.IgnoreCase | MatchFlags.Loose:
				return "LooseAndIgnoreCase";
			case MatchFlags.IgnoreNonSpace | MatchFlags.Loose:
				return "LooseAndIgnoreNonSpace";
			case MatchFlags.IgnoreCase | MatchFlags.IgnoreNonSpace | MatchFlags.Loose:
				return "LooseAndIgnoreCaseAndIgnoreNonSpace";
			default:
				return null;
			}
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x00050EA0 File Offset: 0x0004F0A0
		public static MatchOptions ExtractMatchOptions(ContainsExpressionType containsExpression)
		{
			if (!containsExpression.ContainmentModeSpecified)
			{
				return MatchOptions.FullString;
			}
			string containmentModeString = containsExpression.ContainmentModeString;
			string key;
			switch (key = containmentModeString)
			{
			case "FullString":
				return MatchOptions.FullString;
			case "Prefixed":
				return MatchOptions.Prefix;
			case "Substring":
				return MatchOptions.SubString;
			case "Suffixed":
				return MatchOptions.Suffix;
			case "PrefixOnWords":
				return MatchOptions.PrefixOnWords;
			case "ExactPhrase":
				return MatchOptions.ExactPhrase;
			}
			return MatchOptions.FullString;
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x00050F64 File Offset: 0x0004F164
		public static MatchFlags ExtractMatchFlags(ContainsExpressionType containsExpression)
		{
			if (!containsExpression.ContainmentComparisonSpecified)
			{
				return MatchFlags.Default;
			}
			string containmentComparisonString = containsExpression.ContainmentComparisonString;
			string key;
			switch (key = containmentComparisonString)
			{
			case "Exact":
				return MatchFlags.Default;
			case "IgnoreCase":
				return MatchFlags.IgnoreCase;
			case "IgnoreNonSpacingCharacters":
				return MatchFlags.IgnoreNonSpace;
			case "Loose":
				return MatchFlags.Loose;
			case "IgnoreCaseAndNonSpacingCharacters":
				return MatchFlags.IgnoreCase | MatchFlags.IgnoreNonSpace;
			case "LooseAndIgnoreCase":
				return MatchFlags.IgnoreCase | MatchFlags.Loose;
			case "LooseAndIgnoreNonSpace":
				return MatchFlags.IgnoreNonSpace | MatchFlags.Loose;
			case "LooseAndIgnoreCaseAndIgnoreNonSpace":
				return MatchFlags.IgnoreCase | MatchFlags.IgnoreNonSpace | MatchFlags.Loose;
			}
			return MatchFlags.Default;
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x0005104A File Offset: 0x0004F24A
		private static AttendeeType AttendeeTypeFromSubFilterType(SubFilterType subFilterType)
		{
			if (subFilterType == SubFilterType.AttendeeOptional)
			{
				return AttendeeType.Optional;
			}
			if (subFilterType == SubFilterType.AttendeeRequired)
			{
				return AttendeeType.Required;
			}
			if (subFilterType == SubFilterType.AttendeeResource)
			{
				return AttendeeType.Resource;
			}
			throw new InvalidRestrictionException(CoreResources.IDs.ErrorInvalidSubfilterTypeNotAttendeeType);
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x0005106D File Offset: 0x0004F26D
		private static RecipientItemType RecipientItemTypeFromSubFilterType(SubFilterType subFilterType)
		{
			if (subFilterType == SubFilterType.RecipientTo)
			{
				return RecipientItemType.To;
			}
			if (subFilterType == SubFilterType.RecipientCc)
			{
				return RecipientItemType.Cc;
			}
			if (subFilterType == SubFilterType.RecipientBcc)
			{
				return RecipientItemType.Bcc;
			}
			throw new InvalidRestrictionException(CoreResources.IDs.ErrorInvalidSubfilterTypeNotRecipientType);
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x00051090 File Offset: 0x0004F290
		private static string GetFieldUriForPropertyElement(XmlElement element)
		{
			XmlAttribute xmlAttribute = (XmlAttribute)element.Attributes.GetNamedItem("FieldURI");
			return xmlAttribute.Value;
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x000510BC File Offset: 0x0004F2BC
		private static SubFilterBuilder.ExpressionClassification GetExpressionClassification(XmlElement element)
		{
			string localName;
			switch (localName = element.LocalName)
			{
			case "Exists":
				return SubFilterBuilder.ExpressionClassification.UnaryExpression;
			case "IsEqualTo":
			case "IsNotEqualTo":
			case "IsGreaterThan":
			case "IsGreaterThanOrEqualTo":
			case "IsLessThan":
			case "IsLessThanOrEqualTo":
			case "Excludes":
			case "Contains":
				return SubFilterBuilder.ExpressionClassification.BinaryExpression;
			case "Not":
				return SubFilterBuilder.ExpressionClassification.UnaryOperatorBooleanExpression;
			case "And":
			case "Or":
				return SubFilterBuilder.ExpressionClassification.MultipleOperatorBooleanExpression;
			}
			return SubFilterBuilder.ExpressionClassification.Unknown;
		}

		// Token: 0x04000C36 RID: 3126
		private static Dictionary<string, SubFilterType> subFilterMap = new Dictionary<string, SubFilterType>();

		// Token: 0x02000282 RID: 642
		private enum ExpressionClassification
		{
			// Token: 0x04000C38 RID: 3128
			UnaryExpression,
			// Token: 0x04000C39 RID: 3129
			BinaryExpression,
			// Token: 0x04000C3A RID: 3130
			UnaryOperatorBooleanExpression,
			// Token: 0x04000C3B RID: 3131
			MultipleOperatorBooleanExpression,
			// Token: 0x04000C3C RID: 3132
			Unknown
		}
	}
}
