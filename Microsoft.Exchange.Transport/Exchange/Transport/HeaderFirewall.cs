using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Mime.Internal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000164 RID: 356
	internal static class HeaderFirewall
	{
		// Token: 0x06000F8A RID: 3978 RVA: 0x0003F234 File Offset: 0x0003D434
		static HeaderFirewall()
		{
			HeaderFirewall.InitializeCrossPremiseHeaderLists();
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x0003F29C File Offset: 0x0003D49C
		public static bool PreserveOutgoingcrossPremisesHeaders(Trace tracer, HeaderList rootPartHeaders)
		{
			return HeaderFirewall.TransformCrossPremisesHeaders(tracer, rootPartHeaders, true, false);
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x0003F2A7 File Offset: 0x0003D4A7
		public static bool PromoteIncomingCrossPremisesHeaders(Trace tracer, HeaderList rootPartHeaders)
		{
			return HeaderFirewall.TransformCrossPremisesHeaders(tracer, rootPartHeaders, false, true);
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x0003F2B4 File Offset: 0x0003D4B4
		public static bool FilterCrossPremisesHeaders(HeaderList headers)
		{
			List<Header> list = new List<Header>();
			foreach (Header header in headers)
			{
				if (header.Name.StartsWith("X-MS-Exchange-CrossPremises-", StringComparison.OrdinalIgnoreCase))
				{
					list.Add(header);
				}
			}
			foreach (Header oldChild in list)
			{
				headers.RemoveChild(oldChild);
			}
			return list.Count != 0;
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x0003F368 File Offset: 0x0003D568
		public static bool CrossPremisesHeadersPresent(HeaderList headers)
		{
			foreach (Header header in headers)
			{
				if (header.Name.StartsWith("X-MS-Exchange-CrossPremises-", StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x0003F3CC File Offset: 0x0003D5CC
		public static bool Filter(HeaderList headers, RestrictedHeaderSet blocked)
		{
			if (blocked == RestrictedHeaderSet.None)
			{
				return false;
			}
			List<Header> list = new List<Header>();
			foreach (Header header in headers)
			{
				if (HeaderFirewall.IsHeaderBlocked(header, blocked))
				{
					list.Add(header);
				}
			}
			for (int i = 0; i < list.Count; i++)
			{
				headers.RemoveChild(list[i]);
			}
			return list.Count > 0;
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x0003F458 File Offset: 0x0003D658
		public static bool ContainsBlockedHeaders(HeaderList headers, RestrictedHeaderSet blocked)
		{
			if (blocked == RestrictedHeaderSet.None)
			{
				return false;
			}
			foreach (Header header in headers)
			{
				if (HeaderFirewall.IsHeaderBlocked(header, blocked))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x0003F4B4 File Offset: 0x0003D6B4
		public static bool IsHeaderBlocked(Header header, RestrictedHeaderSet blocked)
		{
			string name = header.Name;
			if (string.IsNullOrEmpty(name))
			{
				return false;
			}
			if ((blocked & RestrictedHeaderSet.Organization) != RestrictedHeaderSet.None && name.StartsWith("X-MS-Exchange-Organization-", StringComparison.OrdinalIgnoreCase))
			{
				return true;
			}
			if ((blocked & RestrictedHeaderSet.Forest) != RestrictedHeaderSet.None && name.StartsWith("X-MS-Exchange-Forest-", StringComparison.OrdinalIgnoreCase))
			{
				return true;
			}
			if ((blocked & RestrictedHeaderSet.MTA) != RestrictedHeaderSet.None)
			{
				for (int i = 0; i < HeaderFirewall.MtaOnlyHeaders.Length; i++)
				{
					if (name.Equals(HeaderFirewall.MtaOnlyHeaders[i], StringComparison.OrdinalIgnoreCase))
					{
						return true;
					}
				}
				for (int j = 0; j < HeaderFirewall.MtaOnlyNamespaces.Length; j++)
				{
					if (name.StartsWith(HeaderFirewall.MtaOnlyNamespaces[j], StringComparison.OrdinalIgnoreCase))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x0003F54C File Offset: 0x0003D74C
		private static void InitializeCrossPremiseHeaderLists()
		{
			HeaderFirewall.orgHeadersToPreserve = new Dictionary<string, Func<Header, bool>>(StringComparer.OrdinalIgnoreCase);
			HeaderFirewall.orgHeaderPrefixesToPreserve = new List<string>();
			HeaderFirewall.crossPremisesHeadersToPromote = new Dictionary<string, Func<Header, bool>>(StringComparer.OrdinalIgnoreCase);
			HeaderFirewall.crossPremisesHeaderPrefixesToPromote = new List<string>();
			HeaderFirewall.AddHeaderToCrossPremisesHeaderLists("X-MS-Exchange-Organization-PCL");
			HeaderFirewall.AddHeaderToCrossPremisesHeaderLists("X-MS-Exchange-Organization-SCL");
			HeaderFirewall.AddHeaderToCrossPremisesHeaderLists("X-MS-Exchange-Organization-Antispam-Report");
			HeaderFirewall.AddHeaderToCrossPremisesHeaderLists("X-MS-Exchange-Organization-Original-SCL");
			HeaderFirewall.AddHeaderToCrossPremisesHeaderLists("X-MS-Exchange-Organization-Original-Sender");
			HeaderFirewall.AddHeaderToCrossPremisesHeaderLists("X-MS-Exchange-Organization-PRD");
			HeaderFirewall.AddHeaderToCrossPremisesHeaderLists("X-MS-Exchange-Organization-Quarantine");
			HeaderFirewall.AddHeaderToCrossPremisesHeaderLists("X-MS-Exchange-Organization-SenderIdResult");
			HeaderFirewall.AddHeaderToCrossPremisesHeaderLists("X-MS-Exchange-Organization-Antispam-AsyncContext");
			HeaderFirewall.AddHeaderToCrossPremisesHeaderLists("X-MS-Exchange-Organization-Antispam-ScanContext", null, new Func<Header, bool>(HeaderFirewall.AntiSpamContextPromoteHandler));
			HeaderFirewall.AddHeaderToCrossPremisesHeaderLists("X-MS-Exchange-Organization-Antispam-IPv6Check");
			HeaderFirewall.AddHeaderToCrossPremisesHeaderLists("X-MS-Exchange-Organization-Hygiene-ReleasedFromQuarantine");
			HeaderFirewall.AddHeaderToCrossPremisesHeaderLists("X-MS-Exchange-Organization-Approval-Allowed-Decision-Makers");
			HeaderFirewall.AddHeaderToCrossPremisesHeaderLists("X-MS-Exchange-Organization-Approval-Allowed-Actions");
			HeaderFirewall.AddHeaderToCrossPremisesHeaderLists("X-MS-Exchange-Organization-Approval-Requestor");
			HeaderFirewall.AddHeaderToCrossPremisesHeaderLists("X-MS-Exchange-Organization-Approval-Initiator");
			HeaderFirewall.AddHeaderToCrossPremisesHeaderLists("X-MS-Exchange-Organization-Approval-AttachToApprovalRequest");
			HeaderFirewall.AddHeaderToCrossPremisesHeaderLists("X-MS-Exchange-Organization-Approval-Approved");
			HeaderFirewall.AddHeaderToCrossPremisesHeaderLists("X-MS-Exchange-Organization-Moderation-Data");
			HeaderFirewall.AddHeaderToCrossPremisesHeaderLists("X-MS-Exchange-Organization-Bypass-Child-Moderation");
			HeaderFirewall.AddHeaderToCrossPremisesHeaderLists("X-MS-Exchange-Organization-Moderation-SavedArrivalTime");
			HeaderFirewall.AddHeaderToCrossPremisesHeaderLists("X-MS-Exchange-Organization-AuthAs");
			HeaderFirewall.AddHeaderToCrossPremisesHeaderLists("X-MS-Exchange-Organization-AuthSource");
			HeaderFirewall.AddHeaderToCrossPremisesHeaderLists("X-MS-Exchange-Organization-AuthDomain");
			HeaderFirewall.AddHeaderToCrossPremisesHeaderLists("X-MS-Exchange-Organization-AuthMechanism");
			HeaderFirewall.AddHeaderToCrossPremisesHeaderLists("X-MS-Exchange-Organization-History");
			HeaderFirewall.AddHeaderToCrossPremisesHeaderLists("X-MS-Exchange-Organization-DeliveryFolder");
			HeaderFirewall.AddHeaderToCrossPremisesHeaderLists("X-MS-Exchange-Organization-Dsn-Version");
			HeaderFirewall.AddHeaderToCrossPremisesHeaderLists("X-MS-Exchange-Organization-StorageQuota");
			HeaderFirewall.AddHeaderToCrossPremisesHeaderLists("X-MS-Exchange-Organization-Mapi-Admin-Submission");
			HeaderFirewall.AddHeaderToCrossPremisesHeaderLists("X-MS-Exchange-Organization-Recipient-P2-Type");
			HeaderFirewall.AddHeaderToCrossPremisesHeaderLists("X-MS-Exchange-Organization-Journal-Report");
			HeaderFirewall.AddHeaderToCrossPremisesHeaderLists("X-MS-Exchange-Organization-Journaled-To-Recipients");
			HeaderFirewall.AddHeaderToCrossPremisesHeaderLists("X-MS-Exchange-Organization-Journaling-Remote-Accounts");
			HeaderFirewall.AddHeaderToCrossPremisesHeaderLists("X-MS-Exchange-Organization-Do-Not-Journal");
			HeaderFirewall.AddHeaderToCrossPremisesHeaderLists("X-MS-Exchange-Organization-Processed-By-Journaling");
			HeaderFirewall.AddHeaderToCrossPremisesHeaderLists("X-MS-Exchange-Organization-BCC");
			HeaderFirewall.AddHeaderToCrossPremisesHeaderLists("X-MS-Exchange-Organization-AutoForwarded");
			HeaderFirewall.AddHeaderToCrossPremisesHeaderLists("X-MS-Exchange-Organization-Original-Received-Time");
			HeaderFirewall.AddHeaderToCrossPremisesHeaderLists("X-MS-Exchange-Organization-Disclaimer-Hash");
			HeaderFirewall.AddHeaderToCrossPremisesHeaderLists("X-MS-Exchange-Organization-Classification");
			HeaderFirewall.AddHeaderToCrossPremisesHeaderLists("X-MS-Exchange-Organization-MessageSource");
			HeaderFirewall.AddHeaderToCrossPremisesHeaderLists("X-MS-Exchange-Organization-OriginalClientIPAddress");
			HeaderFirewall.AddHeaderToCrossPremisesHeaderLists("X-MS-Exchange-Organization-OutboundConnector");
			HeaderFirewall.AddHeaderToCrossPremisesHeaderLists("X-MS-Exchange-Organization-AVStamp-Service");
			HeaderFirewall.AddHeaderToCrossPremisesHeaderLists("X-MS-Exchange-Organization-Text-Messaging-Mapi-Class");
			HeaderFirewall.AddHeaderToCrossPremisesHeaderLists("X-MS-Exchange-Organization-Text-Messaging-Originator");
			HeaderFirewall.AddHeaderToCrossPremisesHeaderLists("X-MS-Exchange-Organization-Text-Messaging-Count-Of-Settings-Segments");
			HeaderFirewall.AddHeaderToCrossPremisesHeaderLists("X-MS-Exchange-Organization-Text-Messaging-Timestamp");
			HeaderFirewall.AddHeaderToCrossPremisesHeaderLists("X-MS-Exchange-Organization-Text-Messaging-Notification-PreferredCulture");
			HeaderFirewall.AddHeaderPrefixToCrossPremisesHeaderPrefixLists("X-MS-Exchange-Organization-Text-Messaging-Settings-Segment-");
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x0003F796 File Offset: 0x0003D996
		private static void AddHeaderToCrossPremisesHeaderLists(string orgHeaderName)
		{
			HeaderFirewall.AddHeaderToCrossPremisesHeaderLists(orgHeaderName, null, null);
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x0003F7A0 File Offset: 0x0003D9A0
		private static void AddHeaderToCrossPremisesHeaderLists(string orgHeaderName, Func<Header, bool> preserveDelegate, Func<Header, bool> promoteDelegate)
		{
			HeaderFirewall.orgHeadersToPreserve.Add(orgHeaderName, preserveDelegate);
			string oldValue = orgHeaderName.Substring(0, "X-MS-Exchange-Organization-".Length);
			string key = orgHeaderName.Replace(oldValue, "X-MS-Exchange-CrossPremises-");
			HeaderFirewall.crossPremisesHeadersToPromote.Add(key, promoteDelegate);
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x0003F7E4 File Offset: 0x0003D9E4
		private static void AddHeaderPrefixToCrossPremisesHeaderPrefixLists(string orgHeaderPrefix)
		{
			HeaderFirewall.orgHeaderPrefixesToPreserve.Add(orgHeaderPrefix);
			string oldValue = orgHeaderPrefix.Substring(0, "X-MS-Exchange-Organization-".Length);
			string item = orgHeaderPrefix.Replace(oldValue, "X-MS-Exchange-CrossPremises-");
			HeaderFirewall.crossPremisesHeaderPrefixesToPromote.Add(item);
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x0003F828 File Offset: 0x0003DA28
		private static bool TransformCrossPremisesHeaders(Trace tracer, HeaderList rootPartHeaders, bool outgoing, bool removeExisting)
		{
			string text;
			string newValue;
			if (outgoing)
			{
				text = "X-MS-Exchange-Organization-";
				newValue = "X-MS-Exchange-CrossPremises-";
			}
			else
			{
				text = "X-MS-Exchange-CrossPremises-";
				newValue = "X-MS-Exchange-Organization-";
			}
			List<Header> list = new List<Header>();
			foreach (Header header in rootPartHeaders)
			{
				Func<Header, bool> func;
				if (HeaderFirewall.ShouldTransformCrossPremisesHeader(tracer, header, outgoing, out func))
				{
					string oldValue = header.Name.Substring(0, text.Length);
					string name = header.Name.Replace(oldValue, newValue);
					Header header2 = Header.Create(name);
					header.CopyTo(header2);
					if (func == null || func(header2))
					{
						list.Add(header2);
					}
				}
			}
			foreach (Header header3 in list)
			{
				if (removeExisting)
				{
					rootPartHeaders.RemoveAll(header3.Name);
				}
				rootPartHeaders.AppendChild(header3);
			}
			return list.Count != 0;
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x0003F948 File Offset: 0x0003DB48
		private static bool ShouldTransformCrossPremisesHeader(Trace tracer, Header header, bool outgoing, out Func<Header, bool> transformedHeaderHandler)
		{
			transformedHeaderHandler = null;
			CrossPremisesHeaderTransformation crossPremisesHeaderTransformation = HeaderFirewall.crossPremisesHeaderTransformationMode;
			Dictionary<string, Func<Header, bool>> dictionary;
			List<string> prefixList;
			string value;
			if (outgoing)
			{
				dictionary = HeaderFirewall.orgHeadersToPreserve;
				prefixList = HeaderFirewall.orgHeaderPrefixesToPreserve;
				value = "X-MS-Exchange-Organization-";
			}
			else
			{
				dictionary = HeaderFirewall.crossPremisesHeadersToPromote;
				prefixList = HeaderFirewall.crossPremisesHeaderPrefixesToPromote;
				value = "X-MS-Exchange-CrossPremises-";
			}
			bool result = false;
			if (dictionary.TryGetValue(header.Name, out transformedHeaderHandler))
			{
				tracer.TraceDebug<string, string, CrossPremisesHeaderTransformation>(0L, "{0} header {1} because filtering mode={2} and it is in the transformation list", outgoing ? "Preserving organization" : "Promoting cross premises", header.Name, crossPremisesHeaderTransformation);
				result = true;
			}
			else if (crossPremisesHeaderTransformation == CrossPremisesHeaderTransformation.Lenient && header.Name.StartsWith(value, StringComparison.OrdinalIgnoreCase))
			{
				tracer.TraceDebug<string, string, CrossPremisesHeaderTransformation>(0L, "{0} header {1} because filtering mode={2}", outgoing ? "Preserving organization" : "Promoting cross premises", header.Name, crossPremisesHeaderTransformation);
				result = true;
			}
			else if (HeaderFirewall.HeaderStartsWithPrefix(header.Name, prefixList))
			{
				tracer.TraceDebug<string, string, CrossPremisesHeaderTransformation>(0L, "{0} header {1} because filtering mode={2} and it is in the transformation prefix list", outgoing ? "Preserving organization" : "Promoting cross premises", header.Name, crossPremisesHeaderTransformation);
				result = true;
			}
			return result;
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x0003FA34 File Offset: 0x0003DC34
		private static bool HeaderStartsWithPrefix(string headerName, List<string> prefixList)
		{
			foreach (string value in prefixList)
			{
				if (headerName.StartsWith(value, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x0003FA8C File Offset: 0x0003DC8C
		private static bool IsCrossPremisesHeader(string headerName)
		{
			return !string.IsNullOrEmpty(headerName) && headerName.StartsWith("X-MS-Exchange-CrossPremises-", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x0003FAA4 File Offset: 0x0003DCA4
		private static bool AntiSpamContextPromoteHandler(Header header)
		{
			string text = header.Value;
			if (string.IsNullOrEmpty(text))
			{
				return false;
			}
			if (text.IndexOf(MimeConstant.AntispamScanContextXPremTagNameWithSeparator, StringComparison.OrdinalIgnoreCase) == -1)
			{
				if (text[text.Length - 1] != ';')
				{
					text += ';';
				}
				header.Value = string.Concat(new object[]
				{
					text,
					MimeConstant.AntispamScanContextXPremTagNameWithSeparator,
					"true",
					';'
				});
			}
			return true;
		}

		// Token: 0x040007D2 RID: 2002
		public const string OrganizationNamespace = "X-MS-Exchange-Organization-";

		// Token: 0x040007D3 RID: 2003
		public const string ForestNamespace = "X-MS-Exchange-Forest-";

		// Token: 0x040007D4 RID: 2004
		public const string CrossPremisesNamespace = "X-MS-Exchange-CrossPremises-";

		// Token: 0x040007D5 RID: 2005
		public const string DiagnosticHeadersFilteredHeader = "X-CrossPremisesHeadersFilteredBySendConnector";

		// Token: 0x040007D6 RID: 2006
		public static readonly string[] MtaOnlyHeaders = new string[]
		{
			"Received"
		};

		// Token: 0x040007D7 RID: 2007
		public static readonly string[] MtaOnlyNamespaces = new string[]
		{
			"Resent-"
		};

		// Token: 0x040007D8 RID: 2008
		public static readonly string ComputerName = ComputerInformation.DnsPhysicalFullyQualifiedDomainName;

		// Token: 0x040007D9 RID: 2009
		private static Dictionary<string, Func<Header, bool>> orgHeadersToPreserve;

		// Token: 0x040007DA RID: 2010
		private static Dictionary<string, Func<Header, bool>> crossPremisesHeadersToPromote;

		// Token: 0x040007DB RID: 2011
		private static List<string> orgHeaderPrefixesToPreserve;

		// Token: 0x040007DC RID: 2012
		private static List<string> crossPremisesHeaderPrefixesToPromote;

		// Token: 0x040007DD RID: 2013
		private static readonly CrossPremisesHeaderTransformation crossPremisesHeaderTransformationMode = VariantConfiguration.InvariantNoFlightingSnapshot.Transport.StringentHeaderTransformationMode.Enabled ? CrossPremisesHeaderTransformation.Stringent : CrossPremisesHeaderTransformation.Lenient;

		// Token: 0x02000165 RID: 357
		public class OutputFilter : MimeOutputFilter
		{
			// Token: 0x06000F9B RID: 3995 RVA: 0x0003FB23 File Offset: 0x0003DD23
			public OutputFilter(RestrictedHeaderSet blocked) : this(blocked, false, null)
			{
			}

			// Token: 0x06000F9C RID: 3996 RVA: 0x0003FB2E File Offset: 0x0003DD2E
			public OutputFilter(RestrictedHeaderSet blocked, bool downconvert) : this(blocked, downconvert, null)
			{
			}

			// Token: 0x06000F9D RID: 3997 RVA: 0x0003FB39 File Offset: 0x0003DD39
			public OutputFilter(RestrictedHeaderSet blocked, bool downconvert, Header diagHeader)
			{
				this.blocked = blocked;
				this.diagHeader = diagHeader;
				this.isdiagHeaderAdded = false;
				if (downconvert)
				{
					this.eightToSevenBitFilter = new EightToSevenBitConverter.OutputFilter();
				}
			}

			// Token: 0x06000F9E RID: 3998 RVA: 0x0003FB64 File Offset: 0x0003DD64
			public override bool FilterPart(MimePart part, Stream stream)
			{
				if (this.partCount < 2)
				{
					this.partCount++;
				}
				return false;
			}

			// Token: 0x06000F9F RID: 3999 RVA: 0x0003FB7E File Offset: 0x0003DD7E
			public override bool FilterPartBody(MimePart part, Stream stream)
			{
				return this.eightToSevenBitFilter != null && this.eightToSevenBitFilter.FilterPartBody(part, stream);
			}

			// Token: 0x06000FA0 RID: 4000 RVA: 0x0003FB98 File Offset: 0x0003DD98
			public override bool FilterHeaderList(HeaderList headers, Stream stream)
			{
				if (this.eightToSevenBitFilter == null)
				{
					return false;
				}
				if (this.partCount > 1)
				{
					return this.eightToSevenBitFilter.FilterHeaderList(headers, stream);
				}
				HeaderList headerList = (HeaderList)headers.Clone();
				bool flag = false;
				if (this.diagHeader != null)
				{
					flag = HeaderFirewall.FilterCrossPremisesHeaders(headerList);
					if (!this.isdiagHeaderAdded)
					{
						headerList.AppendChild(this.diagHeader);
						this.isdiagHeaderAdded = true;
					}
				}
				flag = (HeaderFirewall.Filter(headerList, this.blocked) || flag);
				if (this.eightToSevenBitFilter.FilterHeaderList(headerList, stream))
				{
					return true;
				}
				if (!flag)
				{
					return false;
				}
				headerList.WriteTo(stream);
				stream.Write(HeaderFirewall.OutputFilter.CrLf, 0, HeaderFirewall.OutputFilter.CrLf.Length);
				return true;
			}

			// Token: 0x06000FA1 RID: 4001 RVA: 0x0003FC44 File Offset: 0x0003DE44
			public override bool FilterHeader(Header header, Stream stream)
			{
				if (this.partCount > 1)
				{
					return false;
				}
				if (this.eightToSevenBitFilter != null)
				{
					return false;
				}
				bool flag = false;
				if (this.diagHeader != null)
				{
					flag = HeaderFirewall.IsCrossPremisesHeader(header.Name);
					if (!this.isdiagHeaderAdded)
					{
						this.diagHeader.WriteTo(stream);
						this.isdiagHeaderAdded = true;
					}
				}
				return HeaderFirewall.IsHeaderBlocked(header, this.blocked) || flag;
			}

			// Token: 0x040007DE RID: 2014
			private static readonly byte[] CrLf = new byte[]
			{
				13,
				10
			};

			// Token: 0x040007DF RID: 2015
			private RestrictedHeaderSet blocked;

			// Token: 0x040007E0 RID: 2016
			private int partCount;

			// Token: 0x040007E1 RID: 2017
			private EightToSevenBitConverter.OutputFilter eightToSevenBitFilter;

			// Token: 0x040007E2 RID: 2018
			private bool isdiagHeaderAdded;

			// Token: 0x040007E3 RID: 2019
			private Header diagHeader;
		}

		// Token: 0x02000166 RID: 358
		internal static class TextMessagingHeaders
		{
			// Token: 0x040007E4 RID: 2020
			public const string XheaderTextMessagingMapiClass = "X-MS-Exchange-Organization-Text-Messaging-Mapi-Class";

			// Token: 0x040007E5 RID: 2021
			public const string XheaderTextMessagingOriginator = "X-MS-Exchange-Organization-Text-Messaging-Originator";

			// Token: 0x040007E6 RID: 2022
			public const string XheaderTextMessagingCountOfSettingsSegments = "X-MS-Exchange-Organization-Text-Messaging-Count-Of-Settings-Segments";

			// Token: 0x040007E7 RID: 2023
			public const string XheaderTextMessagingSettingsSegmentPrefix = "X-MS-Exchange-Organization-Text-Messaging-Settings-Segment-";

			// Token: 0x040007E8 RID: 2024
			public const string XheaderTextMessagingTimestamp = "X-MS-Exchange-Organization-Text-Messaging-Timestamp";

			// Token: 0x040007E9 RID: 2025
			public const string XheaderTextMessagingNotificationPreferredCulture = "X-MS-Exchange-Organization-Text-Messaging-Notification-PreferredCulture";
		}
	}
}
