using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.HttpProxy.Routing.RoutingDestinations;
using Microsoft.Exchange.HttpProxy.Routing.RoutingEntries;
using Microsoft.Exchange.HttpProxy.Routing.RoutingKeys;

namespace Microsoft.Exchange.HttpProxy.Routing.Serialization
{
	// Token: 0x02000044 RID: 68
	public static class RoutingEntryHeaderSerializer
	{
		// Token: 0x06000115 RID: 277 RVA: 0x00004AB8 File Offset: 0x00002CB8
		public static IRoutingEntry Deserialize(string headerValue)
		{
			if (headerValue == null)
			{
				throw new ArgumentNullException("headerValue");
			}
			RoutingEntryHeaderSerializer.RoutingEntryParts routingEntryParts;
			if (!RoutingEntryHeaderSerializer.TrySplitEntry(headerValue, out routingEntryParts))
			{
				throw new ArgumentException("Value is incorrectly formatted", "headerValue");
			}
			IRoutingKey key = RoutingEntryHeaderSerializer.DeserializeRoutingKey(routingEntryParts.KeyType, routingEntryParts.KeyValue);
			IRoutingDestination destination = RoutingEntryHeaderSerializer.DeserializeRoutingDestination(routingEntryParts.DestinationType, routingEntryParts.DestinationValue);
			return RoutingEntryHeaderSerializer.AssembleRoutingEntry(key, destination, routingEntryParts.Timestamp);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00004B24 File Offset: 0x00002D24
		public static bool IsValidHeaderString(string headerValue)
		{
			RoutingEntryHeaderSerializer.RoutingEntryParts routingEntryParts;
			return headerValue != null && RoutingEntryHeaderSerializer.TrySplitEntry(headerValue, out routingEntryParts);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00004B44 File Offset: 0x00002D44
		public static string Serialize(IRoutingEntry routingEntry)
		{
			if (routingEntry == null)
			{
				throw new ArgumentNullException("routingEntry");
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(RoutingEntryHeaderSerializer.SerializableRoutingTypeToString(routingEntry.Key.RoutingItemType));
			stringBuilder.Append(':');
			stringBuilder.Append(Uri.EscapeDataString(routingEntry.Key.Value));
			stringBuilder.Append('=');
			stringBuilder.Append(RoutingEntryHeaderSerializer.SerializableRoutingTypeToString(routingEntry.Destination.RoutingItemType));
			stringBuilder.Append(':');
			stringBuilder.Append(Uri.EscapeDataString(routingEntry.Destination.Value));
			foreach (string value in routingEntry.Destination.Properties)
			{
				stringBuilder.Append('+');
				stringBuilder.Append(value);
			}
			stringBuilder.Append('@');
			stringBuilder.Append(routingEntry.Timestamp);
			return stringBuilder.ToString();
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00004C48 File Offset: 0x00002E48
		public static string RoutingTypeToString(RoutingItemType routingType)
		{
			string result;
			if (!RoutingEntryHeaderSerializer.TryGetDefinedRoutingTypeToString(routingType, out result))
			{
				result = routingType.ToString();
			}
			return result;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00004C6C File Offset: 0x00002E6C
		internal static string SerializableRoutingTypeToString(RoutingItemType routingType)
		{
			if (routingType == RoutingItemType.Unknown)
			{
				throw new NotSupportedException("Cannot serialize routing entries of type Unknown");
			}
			string result;
			if (!RoutingEntryHeaderSerializer.TryGetDefinedRoutingTypeToString(routingType, out result))
			{
				throw new ArgumentException(string.Format("Unrecognized routing type: {0}", routingType));
			}
			return result;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00004CAC File Offset: 0x00002EAC
		internal static bool TryGetDefinedRoutingTypeToString(RoutingItemType routingType, out string routingTypeString)
		{
			routingTypeString = null;
			switch (routingType)
			{
			case RoutingItemType.ArchiveSmtp:
				routingTypeString = "ArchiveSmtp";
				return true;
			case RoutingItemType.DatabaseGuid:
				routingTypeString = "DatabaseGuid";
				return true;
			case RoutingItemType.Error:
				routingTypeString = "Error";
				return true;
			case RoutingItemType.MailboxGuid:
				routingTypeString = "MailboxGuid";
				return true;
			case RoutingItemType.Server:
				routingTypeString = "Server";
				return true;
			case RoutingItemType.Smtp:
				routingTypeString = "Smtp";
				return true;
			case RoutingItemType.ExternalDirectoryObjectId:
				routingTypeString = "Oid";
				return true;
			case RoutingItemType.LiveIdMemberName:
				routingTypeString = "LiveIdMemberName";
				return true;
			case RoutingItemType.Unknown:
				routingTypeString = "Unknown";
				return true;
			default:
				return false;
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00004D3C File Offset: 0x00002F3C
		private static IRoutingEntry AssembleRoutingEntry(IRoutingKey key, IRoutingDestination destination, long timestamp)
		{
			if (key is ServerRoutingKey)
			{
				if (destination is ServerRoutingDestination)
				{
					return new SuccessfulServerRoutingEntry((ServerRoutingKey)key, (ServerRoutingDestination)destination, timestamp);
				}
				if (destination is ErrorRoutingDestination)
				{
					return new FailedServerRoutingEntry((ServerRoutingKey)key, (ErrorRoutingDestination)destination, timestamp);
				}
			}
			if (key is DatabaseGuidRoutingKey)
			{
				if (destination is ServerRoutingDestination)
				{
					return new SuccessfulDatabaseGuidRoutingEntry((DatabaseGuidRoutingKey)key, (ServerRoutingDestination)destination, timestamp);
				}
				if (destination is ErrorRoutingDestination)
				{
					return new FailedDatabaseGuidRoutingEntry((DatabaseGuidRoutingKey)key, (ErrorRoutingDestination)destination, timestamp);
				}
			}
			else if (key is ArchiveSmtpRoutingKey || key is MailboxGuidRoutingKey || key is SmtpRoutingKey || key is ExternalDirectoryObjectIdRoutingKey || key is LiveIdMemberNameRoutingKey)
			{
				if (destination is DatabaseGuidRoutingDestination)
				{
					return new SuccessfulMailboxRoutingEntry(key, (DatabaseGuidRoutingDestination)destination, timestamp);
				}
				if (destination is ErrorRoutingDestination)
				{
					return new FailedMailboxRoutingEntry(key, (ErrorRoutingDestination)destination, timestamp);
				}
			}
			return new GenericRoutingEntry(key, destination, timestamp);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00004E24 File Offset: 0x00003024
		private static IRoutingDestination DeserializeRoutingDestination(string type, string value)
		{
			int num = value.IndexOf('+');
			string[] properties = Array<string>.Empty;
			if (num != -1)
			{
				properties = value.Substring(num + 1).Split(new char[]
				{
					'+'
				});
				value = value.Substring(0, num);
			}
			if (type != null)
			{
				DatabaseGuidRoutingDestination result3;
				if (!(type == "DatabaseGuid"))
				{
					ErrorRoutingDestination result2;
					if (!(type == "Error"))
					{
						if (type == "Server")
						{
							ServerRoutingDestination result;
							if (ServerRoutingDestination.TryParse(value, properties, out result))
							{
								return result;
							}
						}
					}
					else if (ErrorRoutingDestination.TryParse(value, properties, out result2))
					{
						return result2;
					}
				}
				else if (DatabaseGuidRoutingDestination.TryParse(value, properties, out result3))
				{
					return result3;
				}
			}
			return new UnknownRoutingDestination(type, value, properties);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00004ED0 File Offset: 0x000030D0
		private static IRoutingKey DeserializeRoutingKey(string type, string value)
		{
			switch (type)
			{
			case "ArchiveSmtp":
			{
				ArchiveSmtpRoutingKey result;
				if (ArchiveSmtpRoutingKey.TryParse(value, out result))
				{
					return result;
				}
				break;
			}
			case "DatabaseGuid":
			{
				DatabaseGuidRoutingKey result2;
				if (DatabaseGuidRoutingKey.TryParse(value, out result2))
				{
					return result2;
				}
				break;
			}
			case "MailboxGuid":
			{
				MailboxGuidRoutingKey result3;
				if (MailboxGuidRoutingKey.TryParse(value, out result3))
				{
					return result3;
				}
				break;
			}
			case "Smtp":
			{
				SmtpRoutingKey result4;
				if (SmtpRoutingKey.TryParse(value, out result4))
				{
					return result4;
				}
				break;
			}
			case "Server":
			{
				ServerRoutingKey result5;
				if (ServerRoutingKey.TryParse(value, out result5))
				{
					return result5;
				}
				break;
			}
			case "Oid":
			{
				ExternalDirectoryObjectIdRoutingKey result6;
				if (ExternalDirectoryObjectIdRoutingKey.TryParse(value, out result6))
				{
					return result6;
				}
				break;
			}
			case "LiveIdMemberName":
			{
				LiveIdMemberNameRoutingKey result7;
				if (LiveIdMemberNameRoutingKey.TryParse(value, out result7))
				{
					return result7;
				}
				break;
			}
			}
			return new UnknownRoutingKey(type, value);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00004FE8 File Offset: 0x000031E8
		private static bool TrySplitEntry(string entryString, out RoutingEntryHeaderSerializer.RoutingEntryParts parts)
		{
			parts = default(RoutingEntryHeaderSerializer.RoutingEntryParts);
			int num = entryString.IndexOf('=');
			if (num == -1)
			{
				return false;
			}
			int num2 = entryString.IndexOf('@', num);
			if (num2 == -1)
			{
				return false;
			}
			string text = entryString.Substring(0, num);
			string text2 = entryString.Substring(num + 1, num2 - num - 1);
			int num3 = text.IndexOf(':');
			if (num3 == -1)
			{
				return false;
			}
			parts.KeyType = text.Substring(0, num3);
			parts.KeyValue = Uri.UnescapeDataString(text.Substring(num3 + 1));
			num3 = text2.IndexOf(':');
			if (num3 == -1)
			{
				return false;
			}
			parts.DestinationType = text2.Substring(0, num3);
			parts.DestinationValue = Uri.UnescapeDataString(text2.Substring(num3 + 1));
			long timestamp;
			if (!long.TryParse(entryString.Substring(num2 + 1), out timestamp))
			{
				return false;
			}
			parts.Timestamp = timestamp;
			return true;
		}

		// Token: 0x04000071 RID: 113
		private const char DestinationSeparator = '=';

		// Token: 0x04000072 RID: 114
		private const char TimestampSeparator = '@';

		// Token: 0x04000073 RID: 115
		private const char TypeSeparator = ':';

		// Token: 0x04000074 RID: 116
		private const char PropertiesSeparator = '+';

		// Token: 0x04000075 RID: 117
		private const string ArchiveSmtpTypeString = "ArchiveSmtp";

		// Token: 0x04000076 RID: 118
		private const string DatabaseGuidTypeString = "DatabaseGuid";

		// Token: 0x04000077 RID: 119
		private const string ErrorTypeString = "Error";

		// Token: 0x04000078 RID: 120
		private const string MailboxGuidTypeString = "MailboxGuid";

		// Token: 0x04000079 RID: 121
		private const string ServerTypeString = "Server";

		// Token: 0x0400007A RID: 122
		private const string SmtpTypeString = "Smtp";

		// Token: 0x0400007B RID: 123
		private const string ExternalDirectoryObjectIdTypeString = "Oid";

		// Token: 0x0400007C RID: 124
		private const string LiveIdMemberNameTypeString = "LiveIdMemberName";

		// Token: 0x0400007D RID: 125
		private const string UnknownTypeString = "Unknown";

		// Token: 0x02000045 RID: 69
		private struct RoutingEntryParts
		{
			// Token: 0x17000059 RID: 89
			// (get) Token: 0x0600011F RID: 287 RVA: 0x000050BA File Offset: 0x000032BA
			// (set) Token: 0x06000120 RID: 288 RVA: 0x000050C2 File Offset: 0x000032C2
			public string KeyType { get; set; }

			// Token: 0x1700005A RID: 90
			// (get) Token: 0x06000121 RID: 289 RVA: 0x000050CB File Offset: 0x000032CB
			// (set) Token: 0x06000122 RID: 290 RVA: 0x000050D3 File Offset: 0x000032D3
			public string KeyValue { get; set; }

			// Token: 0x1700005B RID: 91
			// (get) Token: 0x06000123 RID: 291 RVA: 0x000050DC File Offset: 0x000032DC
			// (set) Token: 0x06000124 RID: 292 RVA: 0x000050E4 File Offset: 0x000032E4
			public string DestinationType { get; set; }

			// Token: 0x1700005C RID: 92
			// (get) Token: 0x06000125 RID: 293 RVA: 0x000050ED File Offset: 0x000032ED
			// (set) Token: 0x06000126 RID: 294 RVA: 0x000050F5 File Offset: 0x000032F5
			public string DestinationValue { get; set; }

			// Token: 0x1700005D RID: 93
			// (get) Token: 0x06000127 RID: 295 RVA: 0x000050FE File Offset: 0x000032FE
			// (set) Token: 0x06000128 RID: 296 RVA: 0x00005106 File Offset: 0x00003306
			public long Timestamp { get; set; }
		}
	}
}
