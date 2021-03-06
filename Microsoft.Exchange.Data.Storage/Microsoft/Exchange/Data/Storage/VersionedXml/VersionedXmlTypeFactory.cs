using System;
using System.IO;
using System.Reflection;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Xml.Serialization.CalendarNotificationContentVersion1Point0;
using Microsoft.Xml.Serialization.CalendarNotificationSettingsVersion1Point0;
using Microsoft.Xml.Serialization.TextMessagingSettingsVersion1Point0;
using Microsoft.Xml.Serialization.WorkHoursInCalendar;

namespace Microsoft.Exchange.Data.Storage.VersionedXml
{
	// Token: 0x02000ED7 RID: 3799
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class VersionedXmlTypeFactory
	{
		// Token: 0x06008328 RID: 33576 RVA: 0x0023A854 File Offset: 0x00238A54
		public static XmlSerializer GetXmlSerializer(Type type)
		{
			XmlSchema xmlSchema = null;
			return VersionedXmlTypeFactory.GetXmlSerializer(type, out xmlSchema);
		}

		// Token: 0x06008329 RID: 33577 RVA: 0x0023A86C File Offset: 0x00238A6C
		public static XmlSerializer GetXmlSerializer(Type type, out XmlSchema schema)
		{
			if (null == type)
			{
				throw new ArgumentNullException("type");
			}
			schema = null;
			if (typeof(TextMessagingSettingsVersion1Point0) == type)
			{
				return new TextMessagingSettingsVersion1Point0Serializer();
			}
			if (typeof(CalendarNotificationSettingsVersion1Point0) == type)
			{
				return new CalendarNotificationSettingsVersion1Point0Serializer();
			}
			if (typeof(CalendarNotificationContentVersion1Point0) == type)
			{
				using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("CalendarNotificationContent.xsd"))
				{
					schema = XmlSchema.Read(manifestResourceStream, null);
				}
				return new CalendarNotificationContentVersion1Point0Serializer();
			}
			if (typeof(WorkHoursInCalendar) == type)
			{
				return new WorkHoursInCalendarSerializer();
			}
			return new XmlSerializer(type);
		}

		// Token: 0x0600832A RID: 33578 RVA: 0x0023A92C File Offset: 0x00238B2C
		public static Type GetTypeInstance(string category, Version version)
		{
			if (string.IsNullOrEmpty(category))
			{
				throw new CustomSerializationException(ServerStrings.ErrorInvalidConfigurationXml, new ArgumentNullException("category"));
			}
			if (null == version)
			{
				new CustomSerializationException(ServerStrings.ErrorUnsupportedConfigurationXmlCategory(category), new ArgumentNullException("version"));
			}
			category = category.Trim();
			if (string.Equals("TextMessagingSettings", category, StringComparison.Ordinal))
			{
				int major = version.Major;
				if (major != 1)
				{
					throw new CustomSerializationException(ServerStrings.ErrorUnsupportedConfigurationXmlVersion(category, version.ToString(2)));
				}
				if (0 <= version.Minor)
				{
					return typeof(TextMessagingSettingsVersion1Point0);
				}
			}
			else if (string.Equals("CalendarNotificationSettings", category, StringComparison.Ordinal))
			{
				int major2 = version.Major;
				if (major2 != 1)
				{
					throw new CustomSerializationException(ServerStrings.ErrorUnsupportedConfigurationXmlVersion(category, version.ToString(2)));
				}
				if (0 <= version.Minor)
				{
					return typeof(CalendarNotificationSettingsVersion1Point0);
				}
			}
			else if (string.Equals("CalendarNotificationContent", category, StringComparison.Ordinal))
			{
				int major3 = version.Major;
				if (major3 != 1)
				{
					throw new CustomSerializationException(ServerStrings.ErrorUnsupportedConfigurationXmlVersion(category, version.ToString(2)));
				}
				if (0 <= version.Minor)
				{
					return typeof(CalendarNotificationContentVersion1Point0);
				}
			}
			throw new CustomSerializationException(ServerStrings.ErrorUnsupportedConfigurationXmlCategory(category));
		}

		// Token: 0x040057E2 RID: 22498
		private const string CalendarNotificationContentSchema = "CalendarNotificationContent.xsd";

		// Token: 0x040057E3 RID: 22499
		internal const string CategoryTextMessageSettings = "TextMessagingSettings";

		// Token: 0x040057E4 RID: 22500
		internal const string CategoryCalendarNotificationSettings = "CalendarNotificationSettings";

		// Token: 0x040057E5 RID: 22501
		internal const string CategoryCalendarNotificationContent = "CalendarNotificationContent";
	}
}
