using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;

namespace Microsoft.Exchange.MessagingPolicies.AttachFilter
{
	// Token: 0x02000007 RID: 7
	internal sealed class Configuration
	{
		// Token: 0x06000019 RID: 25 RVA: 0x00002D5C File Offset: 0x00000F5C
		private void Load()
		{
			Exception ex = null;
			try
			{
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.FullyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 134, "Load", "f:\\15.00.1497\\sources\\dev\\MessagingPolicies\\src\\attachfilter\\Configuration.cs");
				ADObjectId orgContainerId = tenantOrTopologyConfigurationSession.GetOrgContainerId();
				AttachmentFilteringConfig[] array = tenantOrTopologyConfigurationSession.Find<AttachmentFilteringConfig>(orgContainerId, QueryScope.OneLevel, null, null, 1);
				if (array.Length != 1)
				{
					throw new InvalidDataException("Corrupt configuration - there are multiple attachment-filtering configuration objects");
				}
				foreach (string storedAttribute in array[0].AttachmentNames)
				{
					AttachmentFilterEntrySpecification attachmentFilterEntrySpecification = AttachmentFilterEntrySpecification.Parse(storedAttribute);
					if (attachmentFilterEntrySpecification.Type == AttachmentType.FileName)
					{
						this.ProcessFileSpec(attachmentFilterEntrySpecification.Name);
					}
					else
					{
						this.blockedContentTypes.Add(attachmentFilterEntrySpecification.Name);
					}
				}
				foreach (ADObjectId entryId in array[0].ExceptionConnectors)
				{
					ReceiveConnector receiveConnector = tenantOrTopologyConfigurationSession.Read<ReceiveConnector>(entryId);
					if (receiveConnector != null)
					{
						this.exceptionConnectors.Add(receiveConnector.Name);
						ExTraceGlobals.AttachmentFilteringTracer.TraceDebug<string>((long)this.GetHashCode(), "Adding connector {0} as attachment filter exception.", receiveConnector.Name);
					}
				}
				this.action = array[0].Action;
				this.rejectResponse = new SmtpResponse("550", "5.7.1", new string[]
				{
					array[0].RejectResponse
				});
				this.isAdminMessageUsAscii = true;
				foreach (char c in array[0].AdminMessage)
				{
					if (c >= 'Ā')
					{
						this.isAdminMessageUsAscii = false;
						break;
					}
				}
				if (this.isAdminMessageUsAscii)
				{
					this.adminMessage = array[0].AdminMessage.ToCharArray();
				}
				else
				{
					this.adminMessage = new char[array[0].AdminMessage.Length + 1];
					this.adminMessage[0] = '﻿';
					array[0].AdminMessage.CopyTo(0, this.adminMessage, 1, array[0].AdminMessage.Length);
				}
			}
			catch (InvalidDataException ex2)
			{
				ex = ex2;
			}
			catch (DataValidationException ex3)
			{
				ex = ex3;
			}
			catch (ExchangeConfigurationException ex4)
			{
				ex = ex4;
			}
			if (ex != null)
			{
				ExTraceGlobals.AttachmentFilteringTracer.TraceError<string>((long)this.GetHashCode(), "The current configuration is invalid, it will not be loaded. Error message: {0}", ex.ToString());
				this.validConfig = false;
				return;
			}
			this.validConfig = true;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000302C File Offset: 0x0000122C
		private void ProcessFileSpec(string fileSpec)
		{
			string text;
			Regex regex;
			string item;
			AttachmentFilterEntrySpecification.ParseFileSpec(fileSpec, out text, out regex, out item);
			if (text != null)
			{
				this.blockedExtensions.Add(text);
				return;
			}
			if (regex != null)
			{
				this.blockedNameExpressions.Add(regex);
				return;
			}
			this.blockedNames.Add(item);
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00003071 File Offset: 0x00001271
		internal static Configuration Current
		{
			get
			{
				return Configuration.currentConfig;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00003078 File Offset: 0x00001278
		public FilterActions FilterAction
		{
			get
			{
				return this.action;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00003080 File Offset: 0x00001280
		internal SmtpResponse RejectResponse
		{
			get
			{
				return this.rejectResponse;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00003088 File Offset: 0x00001288
		internal char[] AdminMessage
		{
			get
			{
				return this.adminMessage;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00003090 File Offset: 0x00001290
		internal bool IsAdminMessageUsAscii
		{
			get
			{
				return this.isAdminMessageUsAscii;
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00003098 File Offset: 0x00001298
		public static void RegisterConfigurationChangeHandlers()
		{
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 337, "RegisterConfigurationChangeHandlers", "f:\\15.00.1497\\sources\\dev\\MessagingPolicies\\src\\attachfilter\\Configuration.cs");
			ADObjectId orgContainerId = tenantOrTopologyConfigurationSession.GetOrgContainerId();
			ADNotificationAdapter.RegisterChangeNotification<AttachmentFilteringConfig>(orgContainerId, new ADNotificationCallback(Configuration.Configure));
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00003100 File Offset: 0x00001300
		public static void Configure(ADNotificationEventArgs args)
		{
			Configuration newConfig = null;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				newConfig = new Configuration();
				newConfig.Load();
			});
			if (!adoperationResult.Succeeded || newConfig == null || !newConfig.validConfig)
			{
				Agent.Logger.LogEvent(MessagingPoliciesEventLogConstants.Tuple_AttachFilterConfigCorrupt, string.Empty, new object[0]);
				return;
			}
			Agent.Logger.LogEvent(MessagingPoliciesEventLogConstants.Tuple_AttachFilterConfigLoaded, string.Empty, new object[0]);
			Configuration.currentConfig = newConfig;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000318C File Offset: 0x0000138C
		internal SmtpResponse GetSilentDeleteResponse(string messageId)
		{
			return new SmtpResponse("250", "2.6.0", new string[]
			{
				string.Format("{0} Queued mail for delivery.", messageId)
			});
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000031C0 File Offset: 0x000013C0
		internal bool IsBannedName(string name)
		{
			foreach (string text in this.blockedExtensions)
			{
				if (name.EndsWith(text, StringComparison.OrdinalIgnoreCase))
				{
					ExTraceGlobals.AttachmentFilteringTracer.TraceDebug<string>((long)this.GetHashCode(), "Filename extension {0} is blocked", text);
					return true;
				}
			}
			foreach (string text2 in this.blockedNames)
			{
				if (string.Compare(text2, name, StringComparison.OrdinalIgnoreCase) == 0)
				{
					ExTraceGlobals.AttachmentFilteringTracer.TraceDebug<string>((long)this.GetHashCode(), "Filename matches blocked expression {0}", text2);
					return true;
				}
			}
			foreach (Regex regex in this.blockedNameExpressions)
			{
				if (regex.IsMatch(name))
				{
					ExTraceGlobals.AttachmentFilteringTracer.TraceDebug((long)this.GetHashCode(), "Filename matches blocked regex");
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000032F4 File Offset: 0x000014F4
		internal bool IsBannedType(IEnumerable<string> contentTypes)
		{
			foreach (string text in contentTypes)
			{
				ExTraceGlobals.AttachmentFilteringTracer.TraceDebug<string>((long)this.GetHashCode(), "Checking: {0}", text);
				if (this.IsBannedType(text))
				{
					ExTraceGlobals.AttachmentFilteringTracer.TraceDebug((long)this.GetHashCode(), "The Content-Type is illegal");
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00003374 File Offset: 0x00001574
		private bool IsBannedType(string contentType)
		{
			foreach (string text in this.blockedContentTypes)
			{
				if (string.Compare(contentType, text, StringComparison.OrdinalIgnoreCase) == 0)
				{
					ExTraceGlobals.AttachmentFilteringTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Content-type {0} matches blocked content-type {1}", contentType, text);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000033E4 File Offset: 0x000015E4
		public bool IsEnabled(string connectorName)
		{
			foreach (string strB in this.exceptionConnectors)
			{
				if (string.Compare(connectorName, strB, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0400001B RID: 27
		public const int MaxNestedAttachmentDepth = 10;

		// Token: 0x0400001C RID: 28
		public const char UnicodeByteOrderMark = '﻿';

		// Token: 0x0400001D RID: 29
		private static Configuration currentConfig;

		// Token: 0x0400001E RID: 30
		private FilterActions action;

		// Token: 0x0400001F RID: 31
		private IList<string> exceptionConnectors = new List<string>();

		// Token: 0x04000020 RID: 32
		private IList<string> blockedExtensions = new List<string>();

		// Token: 0x04000021 RID: 33
		private IList<string> blockedNames = new List<string>();

		// Token: 0x04000022 RID: 34
		private IList<Regex> blockedNameExpressions = new List<Regex>();

		// Token: 0x04000023 RID: 35
		private IList<string> blockedContentTypes = new List<string>();

		// Token: 0x04000024 RID: 36
		private SmtpResponse rejectResponse;

		// Token: 0x04000025 RID: 37
		private char[] adminMessage;

		// Token: 0x04000026 RID: 38
		private bool isAdminMessageUsAscii;

		// Token: 0x04000027 RID: 39
		private bool validConfig;
	}
}
