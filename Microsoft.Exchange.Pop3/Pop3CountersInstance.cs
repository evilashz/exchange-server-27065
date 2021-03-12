using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Pop3
{
	// Token: 0x02000023 RID: 35
	internal sealed class Pop3CountersInstance : PerformanceCounterInstance
	{
		// Token: 0x06000111 RID: 273 RVA: 0x000066FC File Offset: 0x000048FC
		internal Pop3CountersInstance(string instanceName, Pop3CountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchangePop3")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.PerfCounter_Connections_Current = new ExPerformanceCounter(base.CategoryName, "Connections Current", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_Connections_Current, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_Connections_Current);
				this.PerfCounter_Connections_Failed = new ExPerformanceCounter(base.CategoryName, "Connections Failed", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_Connections_Failed, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_Connections_Failed);
				this.PerfCounter_Connections_Rejected = new ExPerformanceCounter(base.CategoryName, "Connections Rejected", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_Connections_Rejected, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_Connections_Rejected);
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "Unauthenticated Connections/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				this.PerfCounter_UnAuth_Connections_Current = new ExPerformanceCounter(base.CategoryName, "Current Unauthenticated Connections", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_UnAuth_Connections_Current, new ExPerformanceCounter[]
				{
					exPerformanceCounter
				});
				list.Add(this.PerfCounter_UnAuth_Connections_Current);
				this.PerfCounter_Proxy_Connections_Current = new ExPerformanceCounter(base.CategoryName, "Proxy Current Connections", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_Proxy_Connections_Current, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_Proxy_Connections_Current);
				this.PerfCounter_Proxy_Connections_Failed = new ExPerformanceCounter(base.CategoryName, "Proxy Connections Failed", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_Proxy_Connections_Failed, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_Proxy_Connections_Failed);
				this.PerfCounter_Proxy_Connections_Total = new ExPerformanceCounter(base.CategoryName, "Proxy Total Connections", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_Proxy_Connections_Total, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_Proxy_Connections_Total);
				this.PerfCounter_SSLConnections_Current = new ExPerformanceCounter(base.CategoryName, "Active SSL Connections", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_SSLConnections_Current, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_SSLConnections_Current);
				this.PerfCounter_SSLConnections_Total = new ExPerformanceCounter(base.CategoryName, "SSL Connections", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_SSLConnections_Total, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_SSLConnections_Total);
				ExPerformanceCounter exPerformanceCounter2 = new ExPerformanceCounter(base.CategoryName, "Invalid Commands Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter2);
				this.PerfCounter_Invalid_Commands = new ExPerformanceCounter(base.CategoryName, "Invalid Commands", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_Invalid_Commands, new ExPerformanceCounter[]
				{
					exPerformanceCounter2
				});
				list.Add(this.PerfCounter_Invalid_Commands);
				this.PerfCounter_AUTH_Failures = new ExPerformanceCounter(base.CategoryName, "AUTH Failures", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_AUTH_Failures, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_AUTH_Failures);
				ExPerformanceCounter exPerformanceCounter3 = new ExPerformanceCounter(base.CategoryName, "AUTH Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter3);
				this.PerfCounter_AUTH_Total = new ExPerformanceCounter(base.CategoryName, "AUTH Total", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_AUTH_Total, new ExPerformanceCounter[]
				{
					exPerformanceCounter3
				});
				list.Add(this.PerfCounter_AUTH_Total);
				this.PerfCounter_CAPA_Failures = new ExPerformanceCounter(base.CategoryName, "CAPA Failures", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_CAPA_Failures, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_CAPA_Failures);
				ExPerformanceCounter exPerformanceCounter4 = new ExPerformanceCounter(base.CategoryName, "CAPA Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter4);
				this.PerfCounter_CAPA_Total = new ExPerformanceCounter(base.CategoryName, "CAPA Total", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_CAPA_Total, new ExPerformanceCounter[]
				{
					exPerformanceCounter4
				});
				list.Add(this.PerfCounter_CAPA_Total);
				this.PerfCounter_DELE_Failures = new ExPerformanceCounter(base.CategoryName, "DELE Failures", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_DELE_Failures, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_DELE_Failures);
				ExPerformanceCounter exPerformanceCounter5 = new ExPerformanceCounter(base.CategoryName, "DELE Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter5);
				this.PerfCounter_DELE_Total = new ExPerformanceCounter(base.CategoryName, "DELE Total", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_DELE_Total, new ExPerformanceCounter[]
				{
					exPerformanceCounter5
				});
				list.Add(this.PerfCounter_DELE_Total);
				this.PerfCounter_LIST_Failures = new ExPerformanceCounter(base.CategoryName, "LIST Failures", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_LIST_Failures, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_LIST_Failures);
				ExPerformanceCounter exPerformanceCounter6 = new ExPerformanceCounter(base.CategoryName, "LIST Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter6);
				this.PerfCounter_LIST_Total = new ExPerformanceCounter(base.CategoryName, "LIST Total", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_LIST_Total, new ExPerformanceCounter[]
				{
					exPerformanceCounter6
				});
				list.Add(this.PerfCounter_LIST_Total);
				this.PerfCounter_NOOP_Failures = new ExPerformanceCounter(base.CategoryName, "NOOP Failures", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_NOOP_Failures, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_NOOP_Failures);
				ExPerformanceCounter exPerformanceCounter7 = new ExPerformanceCounter(base.CategoryName, "NOOP Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter7);
				this.PerfCounter_NOOP_Total = new ExPerformanceCounter(base.CategoryName, "NOOP Total", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_NOOP_Total, new ExPerformanceCounter[]
				{
					exPerformanceCounter7
				});
				list.Add(this.PerfCounter_NOOP_Total);
				this.PerfCounter_PASS_Failures = new ExPerformanceCounter(base.CategoryName, "PASS Failures", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_PASS_Failures, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_PASS_Failures);
				ExPerformanceCounter exPerformanceCounter8 = new ExPerformanceCounter(base.CategoryName, "PASS Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter8);
				this.PerfCounter_PASS_Total = new ExPerformanceCounter(base.CategoryName, "PASS Total", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_PASS_Total, new ExPerformanceCounter[]
				{
					exPerformanceCounter8
				});
				list.Add(this.PerfCounter_PASS_Total);
				this.PerfCounter_QUIT_Failures = new ExPerformanceCounter(base.CategoryName, "QUIT Failures", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_QUIT_Failures, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_QUIT_Failures);
				ExPerformanceCounter exPerformanceCounter9 = new ExPerformanceCounter(base.CategoryName, "QUIT Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter9);
				this.PerfCounter_QUIT_Total = new ExPerformanceCounter(base.CategoryName, "QUIT Total", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_QUIT_Total, new ExPerformanceCounter[]
				{
					exPerformanceCounter9
				});
				list.Add(this.PerfCounter_QUIT_Total);
				this.PerfCounter_Request_Failures = new ExPerformanceCounter(base.CategoryName, "Request Failures", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_Request_Failures, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_Request_Failures);
				ExPerformanceCounter exPerformanceCounter10 = new ExPerformanceCounter(base.CategoryName, "Request Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter10);
				this.PerfCounter_Request_Total = new ExPerformanceCounter(base.CategoryName, "Request Total", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_Request_Total, new ExPerformanceCounter[]
				{
					exPerformanceCounter10
				});
				list.Add(this.PerfCounter_Request_Total);
				this.PerfCounter_RETR_Failures = new ExPerformanceCounter(base.CategoryName, "RETR Failures", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_RETR_Failures, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_RETR_Failures);
				ExPerformanceCounter exPerformanceCounter11 = new ExPerformanceCounter(base.CategoryName, "RETR Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter11);
				this.PerfCounter_RETR_Total = new ExPerformanceCounter(base.CategoryName, "RETR Total", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_RETR_Total, new ExPerformanceCounter[]
				{
					exPerformanceCounter11
				});
				list.Add(this.PerfCounter_RETR_Total);
				this.PerfCounter_RSET_Failures = new ExPerformanceCounter(base.CategoryName, "RSET Failures", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_RSET_Failures, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_RSET_Failures);
				ExPerformanceCounter exPerformanceCounter12 = new ExPerformanceCounter(base.CategoryName, "RSET Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter12);
				this.PerfCounter_RSET_Total = new ExPerformanceCounter(base.CategoryName, "RSET Total", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_RSET_Total, new ExPerformanceCounter[]
				{
					exPerformanceCounter12
				});
				list.Add(this.PerfCounter_RSET_Total);
				this.PerfCounter_STAT_Failures = new ExPerformanceCounter(base.CategoryName, "STAT Failures", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_STAT_Failures, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_STAT_Failures);
				ExPerformanceCounter exPerformanceCounter13 = new ExPerformanceCounter(base.CategoryName, "STAT Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter13);
				this.PerfCounter_STAT_Total = new ExPerformanceCounter(base.CategoryName, "STAT Total", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_STAT_Total, new ExPerformanceCounter[]
				{
					exPerformanceCounter13
				});
				list.Add(this.PerfCounter_STAT_Total);
				this.PerfCounter_STLS_Failures = new ExPerformanceCounter(base.CategoryName, "STLS Failures", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_STLS_Failures, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_STLS_Failures);
				ExPerformanceCounter exPerformanceCounter14 = new ExPerformanceCounter(base.CategoryName, "STLS Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter14);
				this.PerfCounter_STLS_Total = new ExPerformanceCounter(base.CategoryName, "STLS Total", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_STLS_Total, new ExPerformanceCounter[]
				{
					exPerformanceCounter14
				});
				list.Add(this.PerfCounter_STLS_Total);
				this.PerfCounter_TOP_Failures = new ExPerformanceCounter(base.CategoryName, "TOP Failures", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_TOP_Failures, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_TOP_Failures);
				ExPerformanceCounter exPerformanceCounter15 = new ExPerformanceCounter(base.CategoryName, "TOP Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter15);
				this.PerfCounter_TOP_Total = new ExPerformanceCounter(base.CategoryName, "TOP Total", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_TOP_Total, new ExPerformanceCounter[]
				{
					exPerformanceCounter15
				});
				list.Add(this.PerfCounter_TOP_Total);
				this.PerfCounter_UIDL_Failures = new ExPerformanceCounter(base.CategoryName, "UIDL Failures", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_UIDL_Failures, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_UIDL_Failures);
				ExPerformanceCounter exPerformanceCounter16 = new ExPerformanceCounter(base.CategoryName, "UIDL Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter16);
				this.PerfCounter_UIDL_Total = new ExPerformanceCounter(base.CategoryName, "UIDL Total", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_UIDL_Total, new ExPerformanceCounter[]
				{
					exPerformanceCounter16
				});
				list.Add(this.PerfCounter_UIDL_Total);
				this.PerfCounter_USER_Failures = new ExPerformanceCounter(base.CategoryName, "USER Failures", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_USER_Failures, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_USER_Failures);
				ExPerformanceCounter exPerformanceCounter17 = new ExPerformanceCounter(base.CategoryName, "USER Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter17);
				this.PerfCounter_USER_Total = new ExPerformanceCounter(base.CategoryName, "USER Total", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_USER_Total, new ExPerformanceCounter[]
				{
					exPerformanceCounter17
				});
				list.Add(this.PerfCounter_USER_Total);
				this.PerfCounter_XPRX_Failures = new ExPerformanceCounter(base.CategoryName, "XPRX Failures", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_XPRX_Failures, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_XPRX_Failures);
				ExPerformanceCounter exPerformanceCounter18 = new ExPerformanceCounter(base.CategoryName, "XPRX Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter18);
				this.PerfCounter_XPRX_Total = new ExPerformanceCounter(base.CategoryName, "XPRX Total", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_XPRX_Total, new ExPerformanceCounter[]
				{
					exPerformanceCounter18
				});
				list.Add(this.PerfCounter_XPRX_Total);
				this.PerfCounter_Average_Command_Processing_Time = new ExPerformanceCounter(base.CategoryName, "Average Command Processing Time (milliseconds)", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_Average_Command_Processing_Time, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_Average_Command_Processing_Time);
				ExPerformanceCounter exPerformanceCounter19 = new ExPerformanceCounter(base.CategoryName, "Connections Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter19);
				this.PerfCounter_Connections_Total = new ExPerformanceCounter(base.CategoryName, "Connections Total", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_Connections_Total, new ExPerformanceCounter[]
				{
					exPerformanceCounter19
				});
				list.Add(this.PerfCounter_Connections_Total);
				this.PerfCounter_RatePerMinuteOfTransientMailboxConnectionFailures = new ExPerformanceCounter(base.CategoryName, "Transient Mailbox Connection Failures/minute", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_RatePerMinuteOfTransientMailboxConnectionFailures, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_RatePerMinuteOfTransientMailboxConnectionFailures);
				this.PerfCounter_RatePerMinuteOfMailboxOfflineErrors = new ExPerformanceCounter(base.CategoryName, "Mailbox Offline Errors/minute", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_RatePerMinuteOfMailboxOfflineErrors, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_RatePerMinuteOfMailboxOfflineErrors);
				this.PerfCounter_RatePerMinuteOfTransientStorageErrors = new ExPerformanceCounter(base.CategoryName, "Transient Storage Errors/minute", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_RatePerMinuteOfTransientStorageErrors, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_RatePerMinuteOfTransientStorageErrors);
				this.PerfCounter_RatePerMinuteOfPermanentStorageErrors = new ExPerformanceCounter(base.CategoryName, "Permanent Storage Errors/minute", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_RatePerMinuteOfPermanentStorageErrors, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_RatePerMinuteOfPermanentStorageErrors);
				this.PerfCounter_RatePerMinuteOfTransientActiveDirectoryErrors = new ExPerformanceCounter(base.CategoryName, "Transient Active Directory Errors/minute", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_RatePerMinuteOfTransientActiveDirectoryErrors, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_RatePerMinuteOfTransientActiveDirectoryErrors);
				this.PerfCounter_RatePerMinuteOfPermanentActiveDirectoryErrors = new ExPerformanceCounter(base.CategoryName, "Permanent Active Directory Errors/minute", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_RatePerMinuteOfPermanentActiveDirectoryErrors, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_RatePerMinuteOfPermanentActiveDirectoryErrors);
				this.PerfCounter_RatePerMinuteOfTransientErrors = new ExPerformanceCounter(base.CategoryName, "Transient Errors/minute", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_RatePerMinuteOfTransientErrors, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_RatePerMinuteOfTransientErrors);
				this.PerfCounter_AverageRpcLatency = new ExPerformanceCounter(base.CategoryName, "Average RPC Latency", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_AverageRpcLatency, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_AverageRpcLatency);
				this.PerfCounter_AverageLdapLatency = new ExPerformanceCounter(base.CategoryName, "Average LDAP Latency", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_AverageLdapLatency, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_AverageLdapLatency);
				long num = this.PerfCounter_Connections_Current.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter20 in list)
					{
						exPerformanceCounter20.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000761C File Offset: 0x0000581C
		internal Pop3CountersInstance(string instanceName) : base(instanceName, "MSExchangePop3")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.PerfCounter_Connections_Current = new ExPerformanceCounter(base.CategoryName, "Connections Current", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_Connections_Current);
				this.PerfCounter_Connections_Failed = new ExPerformanceCounter(base.CategoryName, "Connections Failed", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_Connections_Failed);
				this.PerfCounter_Connections_Rejected = new ExPerformanceCounter(base.CategoryName, "Connections Rejected", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_Connections_Rejected);
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "Unauthenticated Connections/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				this.PerfCounter_UnAuth_Connections_Current = new ExPerformanceCounter(base.CategoryName, "Current Unauthenticated Connections", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter
				});
				list.Add(this.PerfCounter_UnAuth_Connections_Current);
				this.PerfCounter_Proxy_Connections_Current = new ExPerformanceCounter(base.CategoryName, "Proxy Current Connections", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_Proxy_Connections_Current);
				this.PerfCounter_Proxy_Connections_Failed = new ExPerformanceCounter(base.CategoryName, "Proxy Connections Failed", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_Proxy_Connections_Failed);
				this.PerfCounter_Proxy_Connections_Total = new ExPerformanceCounter(base.CategoryName, "Proxy Total Connections", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_Proxy_Connections_Total);
				this.PerfCounter_SSLConnections_Current = new ExPerformanceCounter(base.CategoryName, "Active SSL Connections", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_SSLConnections_Current);
				this.PerfCounter_SSLConnections_Total = new ExPerformanceCounter(base.CategoryName, "SSL Connections", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_SSLConnections_Total);
				ExPerformanceCounter exPerformanceCounter2 = new ExPerformanceCounter(base.CategoryName, "Invalid Commands Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter2);
				this.PerfCounter_Invalid_Commands = new ExPerformanceCounter(base.CategoryName, "Invalid Commands", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter2
				});
				list.Add(this.PerfCounter_Invalid_Commands);
				this.PerfCounter_AUTH_Failures = new ExPerformanceCounter(base.CategoryName, "AUTH Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_AUTH_Failures);
				ExPerformanceCounter exPerformanceCounter3 = new ExPerformanceCounter(base.CategoryName, "AUTH Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter3);
				this.PerfCounter_AUTH_Total = new ExPerformanceCounter(base.CategoryName, "AUTH Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter3
				});
				list.Add(this.PerfCounter_AUTH_Total);
				this.PerfCounter_CAPA_Failures = new ExPerformanceCounter(base.CategoryName, "CAPA Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_CAPA_Failures);
				ExPerformanceCounter exPerformanceCounter4 = new ExPerformanceCounter(base.CategoryName, "CAPA Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter4);
				this.PerfCounter_CAPA_Total = new ExPerformanceCounter(base.CategoryName, "CAPA Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter4
				});
				list.Add(this.PerfCounter_CAPA_Total);
				this.PerfCounter_DELE_Failures = new ExPerformanceCounter(base.CategoryName, "DELE Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_DELE_Failures);
				ExPerformanceCounter exPerformanceCounter5 = new ExPerformanceCounter(base.CategoryName, "DELE Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter5);
				this.PerfCounter_DELE_Total = new ExPerformanceCounter(base.CategoryName, "DELE Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter5
				});
				list.Add(this.PerfCounter_DELE_Total);
				this.PerfCounter_LIST_Failures = new ExPerformanceCounter(base.CategoryName, "LIST Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_LIST_Failures);
				ExPerformanceCounter exPerformanceCounter6 = new ExPerformanceCounter(base.CategoryName, "LIST Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter6);
				this.PerfCounter_LIST_Total = new ExPerformanceCounter(base.CategoryName, "LIST Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter6
				});
				list.Add(this.PerfCounter_LIST_Total);
				this.PerfCounter_NOOP_Failures = new ExPerformanceCounter(base.CategoryName, "NOOP Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_NOOP_Failures);
				ExPerformanceCounter exPerformanceCounter7 = new ExPerformanceCounter(base.CategoryName, "NOOP Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter7);
				this.PerfCounter_NOOP_Total = new ExPerformanceCounter(base.CategoryName, "NOOP Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter7
				});
				list.Add(this.PerfCounter_NOOP_Total);
				this.PerfCounter_PASS_Failures = new ExPerformanceCounter(base.CategoryName, "PASS Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_PASS_Failures);
				ExPerformanceCounter exPerformanceCounter8 = new ExPerformanceCounter(base.CategoryName, "PASS Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter8);
				this.PerfCounter_PASS_Total = new ExPerformanceCounter(base.CategoryName, "PASS Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter8
				});
				list.Add(this.PerfCounter_PASS_Total);
				this.PerfCounter_QUIT_Failures = new ExPerformanceCounter(base.CategoryName, "QUIT Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_QUIT_Failures);
				ExPerformanceCounter exPerformanceCounter9 = new ExPerformanceCounter(base.CategoryName, "QUIT Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter9);
				this.PerfCounter_QUIT_Total = new ExPerformanceCounter(base.CategoryName, "QUIT Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter9
				});
				list.Add(this.PerfCounter_QUIT_Total);
				this.PerfCounter_Request_Failures = new ExPerformanceCounter(base.CategoryName, "Request Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_Request_Failures);
				ExPerformanceCounter exPerformanceCounter10 = new ExPerformanceCounter(base.CategoryName, "Request Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter10);
				this.PerfCounter_Request_Total = new ExPerformanceCounter(base.CategoryName, "Request Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter10
				});
				list.Add(this.PerfCounter_Request_Total);
				this.PerfCounter_RETR_Failures = new ExPerformanceCounter(base.CategoryName, "RETR Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_RETR_Failures);
				ExPerformanceCounter exPerformanceCounter11 = new ExPerformanceCounter(base.CategoryName, "RETR Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter11);
				this.PerfCounter_RETR_Total = new ExPerformanceCounter(base.CategoryName, "RETR Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter11
				});
				list.Add(this.PerfCounter_RETR_Total);
				this.PerfCounter_RSET_Failures = new ExPerformanceCounter(base.CategoryName, "RSET Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_RSET_Failures);
				ExPerformanceCounter exPerformanceCounter12 = new ExPerformanceCounter(base.CategoryName, "RSET Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter12);
				this.PerfCounter_RSET_Total = new ExPerformanceCounter(base.CategoryName, "RSET Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter12
				});
				list.Add(this.PerfCounter_RSET_Total);
				this.PerfCounter_STAT_Failures = new ExPerformanceCounter(base.CategoryName, "STAT Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_STAT_Failures);
				ExPerformanceCounter exPerformanceCounter13 = new ExPerformanceCounter(base.CategoryName, "STAT Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter13);
				this.PerfCounter_STAT_Total = new ExPerformanceCounter(base.CategoryName, "STAT Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter13
				});
				list.Add(this.PerfCounter_STAT_Total);
				this.PerfCounter_STLS_Failures = new ExPerformanceCounter(base.CategoryName, "STLS Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_STLS_Failures);
				ExPerformanceCounter exPerformanceCounter14 = new ExPerformanceCounter(base.CategoryName, "STLS Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter14);
				this.PerfCounter_STLS_Total = new ExPerformanceCounter(base.CategoryName, "STLS Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter14
				});
				list.Add(this.PerfCounter_STLS_Total);
				this.PerfCounter_TOP_Failures = new ExPerformanceCounter(base.CategoryName, "TOP Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_TOP_Failures);
				ExPerformanceCounter exPerformanceCounter15 = new ExPerformanceCounter(base.CategoryName, "TOP Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter15);
				this.PerfCounter_TOP_Total = new ExPerformanceCounter(base.CategoryName, "TOP Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter15
				});
				list.Add(this.PerfCounter_TOP_Total);
				this.PerfCounter_UIDL_Failures = new ExPerformanceCounter(base.CategoryName, "UIDL Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_UIDL_Failures);
				ExPerformanceCounter exPerformanceCounter16 = new ExPerformanceCounter(base.CategoryName, "UIDL Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter16);
				this.PerfCounter_UIDL_Total = new ExPerformanceCounter(base.CategoryName, "UIDL Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter16
				});
				list.Add(this.PerfCounter_UIDL_Total);
				this.PerfCounter_USER_Failures = new ExPerformanceCounter(base.CategoryName, "USER Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_USER_Failures);
				ExPerformanceCounter exPerformanceCounter17 = new ExPerformanceCounter(base.CategoryName, "USER Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter17);
				this.PerfCounter_USER_Total = new ExPerformanceCounter(base.CategoryName, "USER Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter17
				});
				list.Add(this.PerfCounter_USER_Total);
				this.PerfCounter_XPRX_Failures = new ExPerformanceCounter(base.CategoryName, "XPRX Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_XPRX_Failures);
				ExPerformanceCounter exPerformanceCounter18 = new ExPerformanceCounter(base.CategoryName, "XPRX Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter18);
				this.PerfCounter_XPRX_Total = new ExPerformanceCounter(base.CategoryName, "XPRX Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter18
				});
				list.Add(this.PerfCounter_XPRX_Total);
				this.PerfCounter_Average_Command_Processing_Time = new ExPerformanceCounter(base.CategoryName, "Average Command Processing Time (milliseconds)", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_Average_Command_Processing_Time);
				ExPerformanceCounter exPerformanceCounter19 = new ExPerformanceCounter(base.CategoryName, "Connections Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter19);
				this.PerfCounter_Connections_Total = new ExPerformanceCounter(base.CategoryName, "Connections Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter19
				});
				list.Add(this.PerfCounter_Connections_Total);
				this.PerfCounter_RatePerMinuteOfTransientMailboxConnectionFailures = new ExPerformanceCounter(base.CategoryName, "Transient Mailbox Connection Failures/minute", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_RatePerMinuteOfTransientMailboxConnectionFailures);
				this.PerfCounter_RatePerMinuteOfMailboxOfflineErrors = new ExPerformanceCounter(base.CategoryName, "Mailbox Offline Errors/minute", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_RatePerMinuteOfMailboxOfflineErrors);
				this.PerfCounter_RatePerMinuteOfTransientStorageErrors = new ExPerformanceCounter(base.CategoryName, "Transient Storage Errors/minute", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_RatePerMinuteOfTransientStorageErrors);
				this.PerfCounter_RatePerMinuteOfPermanentStorageErrors = new ExPerformanceCounter(base.CategoryName, "Permanent Storage Errors/minute", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_RatePerMinuteOfPermanentStorageErrors);
				this.PerfCounter_RatePerMinuteOfTransientActiveDirectoryErrors = new ExPerformanceCounter(base.CategoryName, "Transient Active Directory Errors/minute", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_RatePerMinuteOfTransientActiveDirectoryErrors);
				this.PerfCounter_RatePerMinuteOfPermanentActiveDirectoryErrors = new ExPerformanceCounter(base.CategoryName, "Permanent Active Directory Errors/minute", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_RatePerMinuteOfPermanentActiveDirectoryErrors);
				this.PerfCounter_RatePerMinuteOfTransientErrors = new ExPerformanceCounter(base.CategoryName, "Transient Errors/minute", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_RatePerMinuteOfTransientErrors);
				this.PerfCounter_AverageRpcLatency = new ExPerformanceCounter(base.CategoryName, "Average RPC Latency", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_AverageRpcLatency);
				this.PerfCounter_AverageLdapLatency = new ExPerformanceCounter(base.CategoryName, "Average LDAP Latency", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_AverageLdapLatency);
				long num = this.PerfCounter_Connections_Current.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter20 in list)
					{
						exPerformanceCounter20.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000082F4 File Offset: 0x000064F4
		public override void GetPerfCounterDiagnosticsInfo(XElement topElement)
		{
			XElement xelement = null;
			foreach (ExPerformanceCounter exPerformanceCounter in this.counters)
			{
				try
				{
					if (xelement == null)
					{
						xelement = new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.InstanceName));
						topElement.Add(xelement);
					}
					xelement.Add(new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.CounterName), exPerformanceCounter.NextValue()));
				}
				catch (XmlException ex)
				{
					XElement content = new XElement("Error", ex.Message);
					topElement.Add(content);
				}
			}
		}

		// Token: 0x040000AE RID: 174
		public readonly ExPerformanceCounter PerfCounter_Connections_Current;

		// Token: 0x040000AF RID: 175
		public readonly ExPerformanceCounter PerfCounter_Connections_Failed;

		// Token: 0x040000B0 RID: 176
		public readonly ExPerformanceCounter PerfCounter_Connections_Rejected;

		// Token: 0x040000B1 RID: 177
		public readonly ExPerformanceCounter PerfCounter_Connections_Total;

		// Token: 0x040000B2 RID: 178
		public readonly ExPerformanceCounter PerfCounter_UnAuth_Connections_Current;

		// Token: 0x040000B3 RID: 179
		public readonly ExPerformanceCounter PerfCounter_Proxy_Connections_Current;

		// Token: 0x040000B4 RID: 180
		public readonly ExPerformanceCounter PerfCounter_Proxy_Connections_Failed;

		// Token: 0x040000B5 RID: 181
		public readonly ExPerformanceCounter PerfCounter_Proxy_Connections_Total;

		// Token: 0x040000B6 RID: 182
		public readonly ExPerformanceCounter PerfCounter_SSLConnections_Current;

		// Token: 0x040000B7 RID: 183
		public readonly ExPerformanceCounter PerfCounter_SSLConnections_Total;

		// Token: 0x040000B8 RID: 184
		public readonly ExPerformanceCounter PerfCounter_Invalid_Commands;

		// Token: 0x040000B9 RID: 185
		public readonly ExPerformanceCounter PerfCounter_AUTH_Failures;

		// Token: 0x040000BA RID: 186
		public readonly ExPerformanceCounter PerfCounter_AUTH_Total;

		// Token: 0x040000BB RID: 187
		public readonly ExPerformanceCounter PerfCounter_CAPA_Failures;

		// Token: 0x040000BC RID: 188
		public readonly ExPerformanceCounter PerfCounter_CAPA_Total;

		// Token: 0x040000BD RID: 189
		public readonly ExPerformanceCounter PerfCounter_DELE_Failures;

		// Token: 0x040000BE RID: 190
		public readonly ExPerformanceCounter PerfCounter_DELE_Total;

		// Token: 0x040000BF RID: 191
		public readonly ExPerformanceCounter PerfCounter_LIST_Failures;

		// Token: 0x040000C0 RID: 192
		public readonly ExPerformanceCounter PerfCounter_LIST_Total;

		// Token: 0x040000C1 RID: 193
		public readonly ExPerformanceCounter PerfCounter_NOOP_Failures;

		// Token: 0x040000C2 RID: 194
		public readonly ExPerformanceCounter PerfCounter_NOOP_Total;

		// Token: 0x040000C3 RID: 195
		public readonly ExPerformanceCounter PerfCounter_PASS_Failures;

		// Token: 0x040000C4 RID: 196
		public readonly ExPerformanceCounter PerfCounter_PASS_Total;

		// Token: 0x040000C5 RID: 197
		public readonly ExPerformanceCounter PerfCounter_QUIT_Failures;

		// Token: 0x040000C6 RID: 198
		public readonly ExPerformanceCounter PerfCounter_QUIT_Total;

		// Token: 0x040000C7 RID: 199
		public readonly ExPerformanceCounter PerfCounter_Request_Failures;

		// Token: 0x040000C8 RID: 200
		public readonly ExPerformanceCounter PerfCounter_Request_Total;

		// Token: 0x040000C9 RID: 201
		public readonly ExPerformanceCounter PerfCounter_RETR_Failures;

		// Token: 0x040000CA RID: 202
		public readonly ExPerformanceCounter PerfCounter_RETR_Total;

		// Token: 0x040000CB RID: 203
		public readonly ExPerformanceCounter PerfCounter_RSET_Failures;

		// Token: 0x040000CC RID: 204
		public readonly ExPerformanceCounter PerfCounter_RSET_Total;

		// Token: 0x040000CD RID: 205
		public readonly ExPerformanceCounter PerfCounter_STAT_Failures;

		// Token: 0x040000CE RID: 206
		public readonly ExPerformanceCounter PerfCounter_STAT_Total;

		// Token: 0x040000CF RID: 207
		public readonly ExPerformanceCounter PerfCounter_STLS_Failures;

		// Token: 0x040000D0 RID: 208
		public readonly ExPerformanceCounter PerfCounter_STLS_Total;

		// Token: 0x040000D1 RID: 209
		public readonly ExPerformanceCounter PerfCounter_TOP_Failures;

		// Token: 0x040000D2 RID: 210
		public readonly ExPerformanceCounter PerfCounter_TOP_Total;

		// Token: 0x040000D3 RID: 211
		public readonly ExPerformanceCounter PerfCounter_UIDL_Failures;

		// Token: 0x040000D4 RID: 212
		public readonly ExPerformanceCounter PerfCounter_UIDL_Total;

		// Token: 0x040000D5 RID: 213
		public readonly ExPerformanceCounter PerfCounter_USER_Failures;

		// Token: 0x040000D6 RID: 214
		public readonly ExPerformanceCounter PerfCounter_USER_Total;

		// Token: 0x040000D7 RID: 215
		public readonly ExPerformanceCounter PerfCounter_XPRX_Failures;

		// Token: 0x040000D8 RID: 216
		public readonly ExPerformanceCounter PerfCounter_XPRX_Total;

		// Token: 0x040000D9 RID: 217
		public readonly ExPerformanceCounter PerfCounter_Average_Command_Processing_Time;

		// Token: 0x040000DA RID: 218
		public readonly ExPerformanceCounter PerfCounter_RatePerMinuteOfTransientMailboxConnectionFailures;

		// Token: 0x040000DB RID: 219
		public readonly ExPerformanceCounter PerfCounter_RatePerMinuteOfMailboxOfflineErrors;

		// Token: 0x040000DC RID: 220
		public readonly ExPerformanceCounter PerfCounter_RatePerMinuteOfTransientStorageErrors;

		// Token: 0x040000DD RID: 221
		public readonly ExPerformanceCounter PerfCounter_RatePerMinuteOfPermanentStorageErrors;

		// Token: 0x040000DE RID: 222
		public readonly ExPerformanceCounter PerfCounter_RatePerMinuteOfTransientActiveDirectoryErrors;

		// Token: 0x040000DF RID: 223
		public readonly ExPerformanceCounter PerfCounter_RatePerMinuteOfPermanentActiveDirectoryErrors;

		// Token: 0x040000E0 RID: 224
		public readonly ExPerformanceCounter PerfCounter_RatePerMinuteOfTransientErrors;

		// Token: 0x040000E1 RID: 225
		public readonly ExPerformanceCounter PerfCounter_AverageRpcLatency;

		// Token: 0x040000E2 RID: 226
		public readonly ExPerformanceCounter PerfCounter_AverageLdapLatency;
	}
}
