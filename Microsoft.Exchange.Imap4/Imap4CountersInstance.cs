using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x02000051 RID: 81
	internal sealed class Imap4CountersInstance : PerformanceCounterInstance
	{
		// Token: 0x060002D9 RID: 729 RVA: 0x00014824 File Offset: 0x00012A24
		internal Imap4CountersInstance(string instanceName, Imap4CountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchangeImap4")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.PerfCounter_Connections_Current = new ExPerformanceCounter(base.CategoryName, "Current Connections", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_Connections_Current, new ExPerformanceCounter[0]);
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
				this.PerfCounter_APPEND_Failures = new ExPerformanceCounter(base.CategoryName, "Append Failures", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_APPEND_Failures, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_APPEND_Failures);
				ExPerformanceCounter exPerformanceCounter3 = new ExPerformanceCounter(base.CategoryName, "Append Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter3);
				this.PerfCounter_APPEND_Total = new ExPerformanceCounter(base.CategoryName, "Append Total", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_APPEND_Total, new ExPerformanceCounter[]
				{
					exPerformanceCounter3
				});
				list.Add(this.PerfCounter_APPEND_Total);
				this.PerfCounter_AUTHENTICATE_Failures = new ExPerformanceCounter(base.CategoryName, "Authenticate Failures", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_AUTHENTICATE_Failures, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_AUTHENTICATE_Failures);
				ExPerformanceCounter exPerformanceCounter4 = new ExPerformanceCounter(base.CategoryName, "Authenticate Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter4);
				this.PerfCounter_AUTHENTICATE_Total = new ExPerformanceCounter(base.CategoryName, "Authenticate Total", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_AUTHENTICATE_Total, new ExPerformanceCounter[]
				{
					exPerformanceCounter4
				});
				list.Add(this.PerfCounter_AUTHENTICATE_Total);
				this.PerfCounter_CAPABILITY_Failures = new ExPerformanceCounter(base.CategoryName, "Capability Failures", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_CAPABILITY_Failures, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_CAPABILITY_Failures);
				ExPerformanceCounter exPerformanceCounter5 = new ExPerformanceCounter(base.CategoryName, "Capability Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter5);
				this.PerfCounter_CAPABILITY_Total = new ExPerformanceCounter(base.CategoryName, "Capability Total", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_CAPABILITY_Total, new ExPerformanceCounter[]
				{
					exPerformanceCounter5
				});
				list.Add(this.PerfCounter_CAPABILITY_Total);
				this.PerfCounter_CHECK_Failures = new ExPerformanceCounter(base.CategoryName, "Check Failures", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_CHECK_Failures, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_CHECK_Failures);
				ExPerformanceCounter exPerformanceCounter6 = new ExPerformanceCounter(base.CategoryName, "Check Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter6);
				this.PerfCounter_CHECK_Total = new ExPerformanceCounter(base.CategoryName, "Check Total", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_CHECK_Total, new ExPerformanceCounter[]
				{
					exPerformanceCounter6
				});
				list.Add(this.PerfCounter_CHECK_Total);
				this.PerfCounter_CLOSE_Failures = new ExPerformanceCounter(base.CategoryName, "Close Failures", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_CLOSE_Failures, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_CLOSE_Failures);
				ExPerformanceCounter exPerformanceCounter7 = new ExPerformanceCounter(base.CategoryName, "Close Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter7);
				this.PerfCounter_CLOSE_Total = new ExPerformanceCounter(base.CategoryName, "Close Total", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_CLOSE_Total, new ExPerformanceCounter[]
				{
					exPerformanceCounter7
				});
				list.Add(this.PerfCounter_CLOSE_Total);
				this.PerfCounter_COPY_Failures = new ExPerformanceCounter(base.CategoryName, "Copy Failures", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_COPY_Failures, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_COPY_Failures);
				ExPerformanceCounter exPerformanceCounter8 = new ExPerformanceCounter(base.CategoryName, "Copy Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter8);
				this.PerfCounter_COPY_Total = new ExPerformanceCounter(base.CategoryName, "Copy Total", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_COPY_Total, new ExPerformanceCounter[]
				{
					exPerformanceCounter8
				});
				list.Add(this.PerfCounter_COPY_Total);
				this.PerfCounter_CREATE_Failures = new ExPerformanceCounter(base.CategoryName, "Create Failures", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_CREATE_Failures, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_CREATE_Failures);
				ExPerformanceCounter exPerformanceCounter9 = new ExPerformanceCounter(base.CategoryName, "Create Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter9);
				this.PerfCounter_CREATE_Total = new ExPerformanceCounter(base.CategoryName, "Create Total", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_CREATE_Total, new ExPerformanceCounter[]
				{
					exPerformanceCounter9
				});
				list.Add(this.PerfCounter_CREATE_Total);
				this.PerfCounter_DELETE_Failures = new ExPerformanceCounter(base.CategoryName, "Delete Failures", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_DELETE_Failures, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_DELETE_Failures);
				ExPerformanceCounter exPerformanceCounter10 = new ExPerformanceCounter(base.CategoryName, "Delete Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter10);
				this.PerfCounter_DELETE_Total = new ExPerformanceCounter(base.CategoryName, "Delete Total", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_DELETE_Total, new ExPerformanceCounter[]
				{
					exPerformanceCounter10
				});
				list.Add(this.PerfCounter_DELETE_Total);
				this.PerfCounter_EXAMINE_Failures = new ExPerformanceCounter(base.CategoryName, "Examine Failures", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_EXAMINE_Failures, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_EXAMINE_Failures);
				ExPerformanceCounter exPerformanceCounter11 = new ExPerformanceCounter(base.CategoryName, "Examine Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter11);
				this.PerfCounter_EXAMINE_Total = new ExPerformanceCounter(base.CategoryName, "Examine Total", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_EXAMINE_Total, new ExPerformanceCounter[]
				{
					exPerformanceCounter11
				});
				list.Add(this.PerfCounter_EXAMINE_Total);
				this.PerfCounter_EXPUNGE_Failures = new ExPerformanceCounter(base.CategoryName, "Expunge Failures", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_EXPUNGE_Failures, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_EXPUNGE_Failures);
				ExPerformanceCounter exPerformanceCounter12 = new ExPerformanceCounter(base.CategoryName, "Expunge Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter12);
				this.PerfCounter_EXPUNGE_Total = new ExPerformanceCounter(base.CategoryName, "Expunge Total", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_EXPUNGE_Total, new ExPerformanceCounter[]
				{
					exPerformanceCounter12
				});
				list.Add(this.PerfCounter_EXPUNGE_Total);
				this.PerfCounter_FETCH_Failures = new ExPerformanceCounter(base.CategoryName, "Fetch Failures", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_FETCH_Failures, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_FETCH_Failures);
				ExPerformanceCounter exPerformanceCounter13 = new ExPerformanceCounter(base.CategoryName, "Fetch Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter13);
				this.PerfCounter_FETCH_Total = new ExPerformanceCounter(base.CategoryName, "Fetch Total", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_FETCH_Total, new ExPerformanceCounter[]
				{
					exPerformanceCounter13
				});
				list.Add(this.PerfCounter_FETCH_Total);
				this.PerfCounter_IDLE_Failures = new ExPerformanceCounter(base.CategoryName, "Idle Failures", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_IDLE_Failures, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_IDLE_Failures);
				ExPerformanceCounter exPerformanceCounter14 = new ExPerformanceCounter(base.CategoryName, "Idle Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter14);
				this.PerfCounter_IDLE_Total = new ExPerformanceCounter(base.CategoryName, "Idle Total", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_IDLE_Total, new ExPerformanceCounter[]
				{
					exPerformanceCounter14
				});
				list.Add(this.PerfCounter_IDLE_Total);
				this.PerfCounter_LIST_Failures = new ExPerformanceCounter(base.CategoryName, "List Failures", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_LIST_Failures, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_LIST_Failures);
				ExPerformanceCounter exPerformanceCounter15 = new ExPerformanceCounter(base.CategoryName, "List Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter15);
				this.PerfCounter_LIST_Total = new ExPerformanceCounter(base.CategoryName, "List Total", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_LIST_Total, new ExPerformanceCounter[]
				{
					exPerformanceCounter15
				});
				list.Add(this.PerfCounter_LIST_Total);
				this.PerfCounter_LOGIN_Failures = new ExPerformanceCounter(base.CategoryName, "Login Failures", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_LOGIN_Failures, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_LOGIN_Failures);
				ExPerformanceCounter exPerformanceCounter16 = new ExPerformanceCounter(base.CategoryName, "Login Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter16);
				this.PerfCounter_LOGIN_Total = new ExPerformanceCounter(base.CategoryName, "Login Total", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_LOGIN_Total, new ExPerformanceCounter[]
				{
					exPerformanceCounter16
				});
				list.Add(this.PerfCounter_LOGIN_Total);
				this.PerfCounter_LOGOUT_Failures = new ExPerformanceCounter(base.CategoryName, "Logout Failures", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_LOGOUT_Failures, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_LOGOUT_Failures);
				ExPerformanceCounter exPerformanceCounter17 = new ExPerformanceCounter(base.CategoryName, "Logout Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter17);
				this.PerfCounter_LOGOUT_Total = new ExPerformanceCounter(base.CategoryName, "Logout Total", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_LOGOUT_Total, new ExPerformanceCounter[]
				{
					exPerformanceCounter17
				});
				list.Add(this.PerfCounter_LOGOUT_Total);
				this.PerfCounter_LSUB_Failures = new ExPerformanceCounter(base.CategoryName, "LSUB Failures", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_LSUB_Failures, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_LSUB_Failures);
				ExPerformanceCounter exPerformanceCounter18 = new ExPerformanceCounter(base.CategoryName, "LSUB Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter18);
				this.PerfCounter_LSUB_Total = new ExPerformanceCounter(base.CategoryName, "LSUB Total", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_LSUB_Total, new ExPerformanceCounter[]
				{
					exPerformanceCounter18
				});
				list.Add(this.PerfCounter_LSUB_Total);
				this.PerfCounter_MOVE_Failures = new ExPerformanceCounter(base.CategoryName, "Move Failures", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_MOVE_Failures, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_MOVE_Failures);
				ExPerformanceCounter exPerformanceCounter19 = new ExPerformanceCounter(base.CategoryName, "Move Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter19);
				this.PerfCounter_MOVE_Total = new ExPerformanceCounter(base.CategoryName, "Move Total", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_MOVE_Total, new ExPerformanceCounter[]
				{
					exPerformanceCounter19
				});
				list.Add(this.PerfCounter_MOVE_Total);
				this.PerfCounter_ID_Failures = new ExPerformanceCounter(base.CategoryName, "ID Failures", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_ID_Failures, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_ID_Failures);
				ExPerformanceCounter exPerformanceCounter20 = new ExPerformanceCounter(base.CategoryName, "ID Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter20);
				this.PerfCounter_ID_Total = new ExPerformanceCounter(base.CategoryName, "ID Total", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_ID_Total, new ExPerformanceCounter[]
				{
					exPerformanceCounter20
				});
				list.Add(this.PerfCounter_ID_Total);
				this.PerfCounter_NAMESPACE_Failures = new ExPerformanceCounter(base.CategoryName, "Namespace Failures", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_NAMESPACE_Failures, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_NAMESPACE_Failures);
				ExPerformanceCounter exPerformanceCounter21 = new ExPerformanceCounter(base.CategoryName, "Namespace Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter21);
				this.PerfCounter_NAMESPACE_Total = new ExPerformanceCounter(base.CategoryName, "Namespace Total", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_NAMESPACE_Total, new ExPerformanceCounter[]
				{
					exPerformanceCounter21
				});
				list.Add(this.PerfCounter_NAMESPACE_Total);
				this.PerfCounter_NOOP_Failures = new ExPerformanceCounter(base.CategoryName, "NOOP Failures", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_NOOP_Failures, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_NOOP_Failures);
				ExPerformanceCounter exPerformanceCounter22 = new ExPerformanceCounter(base.CategoryName, "NOOP Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter22);
				this.PerfCounter_NOOP_Total = new ExPerformanceCounter(base.CategoryName, "NOOP Total", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_NOOP_Total, new ExPerformanceCounter[]
				{
					exPerformanceCounter22
				});
				list.Add(this.PerfCounter_NOOP_Total);
				this.PerfCounter_RENAME_Failures = new ExPerformanceCounter(base.CategoryName, "Rename Failures", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_RENAME_Failures, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_RENAME_Failures);
				ExPerformanceCounter exPerformanceCounter23 = new ExPerformanceCounter(base.CategoryName, "Rename Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter23);
				this.PerfCounter_RENAME_Total = new ExPerformanceCounter(base.CategoryName, "Rename Total", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_RENAME_Total, new ExPerformanceCounter[]
				{
					exPerformanceCounter23
				});
				list.Add(this.PerfCounter_RENAME_Total);
				this.PerfCounter_Request_Failures = new ExPerformanceCounter(base.CategoryName, "Request Failures", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_Request_Failures, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_Request_Failures);
				ExPerformanceCounter exPerformanceCounter24 = new ExPerformanceCounter(base.CategoryName, "Request Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter24);
				this.PerfCounter_Request_Total = new ExPerformanceCounter(base.CategoryName, "Request Total", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_Request_Total, new ExPerformanceCounter[]
				{
					exPerformanceCounter24
				});
				list.Add(this.PerfCounter_Request_Total);
				this.PerfCounter_SEARCH_Failures = new ExPerformanceCounter(base.CategoryName, "Search Failures", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_SEARCH_Failures, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_SEARCH_Failures);
				ExPerformanceCounter exPerformanceCounter25 = new ExPerformanceCounter(base.CategoryName, "Search Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter25);
				this.PerfCounter_SEARCH_Total = new ExPerformanceCounter(base.CategoryName, "Search Total", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_SEARCH_Total, new ExPerformanceCounter[]
				{
					exPerformanceCounter25
				});
				list.Add(this.PerfCounter_SEARCH_Total);
				this.PerfCounter_SELECT_Failures = new ExPerformanceCounter(base.CategoryName, "Select Failures", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_SELECT_Failures, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_SELECT_Failures);
				ExPerformanceCounter exPerformanceCounter26 = new ExPerformanceCounter(base.CategoryName, "Select Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter26);
				this.PerfCounter_SELECT_Total = new ExPerformanceCounter(base.CategoryName, "Select Total", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_SELECT_Total, new ExPerformanceCounter[]
				{
					exPerformanceCounter26
				});
				list.Add(this.PerfCounter_SELECT_Total);
				this.PerfCounter_STARTTLS_Failures = new ExPerformanceCounter(base.CategoryName, "STARTTLS Failures", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_STARTTLS_Failures, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_STARTTLS_Failures);
				ExPerformanceCounter exPerformanceCounter27 = new ExPerformanceCounter(base.CategoryName, "STARTTLS Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter27);
				this.PerfCounter_STARTTLS_Total = new ExPerformanceCounter(base.CategoryName, "STARTTLS Total", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_STARTTLS_Total, new ExPerformanceCounter[]
				{
					exPerformanceCounter27
				});
				list.Add(this.PerfCounter_STARTTLS_Total);
				this.PerfCounter_STATUS_Failures = new ExPerformanceCounter(base.CategoryName, "Status Failures", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_STATUS_Failures, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_STATUS_Failures);
				ExPerformanceCounter exPerformanceCounter28 = new ExPerformanceCounter(base.CategoryName, "Status Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter28);
				this.PerfCounter_STATUS_Total = new ExPerformanceCounter(base.CategoryName, "Status Total", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_STATUS_Total, new ExPerformanceCounter[]
				{
					exPerformanceCounter28
				});
				list.Add(this.PerfCounter_STATUS_Total);
				this.PerfCounter_STORE_Failures = new ExPerformanceCounter(base.CategoryName, "Store Failures", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_STORE_Failures, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_STORE_Failures);
				ExPerformanceCounter exPerformanceCounter29 = new ExPerformanceCounter(base.CategoryName, "Store Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter29);
				this.PerfCounter_STORE_Total = new ExPerformanceCounter(base.CategoryName, "Store Total", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_STORE_Total, new ExPerformanceCounter[]
				{
					exPerformanceCounter29
				});
				list.Add(this.PerfCounter_STORE_Total);
				this.PerfCounter_SUBSCRIBE_Failures = new ExPerformanceCounter(base.CategoryName, "Subscribe Failures", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_SUBSCRIBE_Failures, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_SUBSCRIBE_Failures);
				ExPerformanceCounter exPerformanceCounter30 = new ExPerformanceCounter(base.CategoryName, "Subscribe Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter30);
				this.PerfCounter_SUBSCRIBE_Total = new ExPerformanceCounter(base.CategoryName, "Subscribe Total", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_SUBSCRIBE_Total, new ExPerformanceCounter[]
				{
					exPerformanceCounter30
				});
				list.Add(this.PerfCounter_SUBSCRIBE_Total);
				this.PerfCounter_UNSUBSCRIBE_Failures = new ExPerformanceCounter(base.CategoryName, "Unsubscribe Failures", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_UNSUBSCRIBE_Failures, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_UNSUBSCRIBE_Failures);
				ExPerformanceCounter exPerformanceCounter31 = new ExPerformanceCounter(base.CategoryName, "Unsubscribe Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter31);
				this.PerfCounter_UNSUBSCRIBE_Total = new ExPerformanceCounter(base.CategoryName, "Unsubscribe Total", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_UNSUBSCRIBE_Total, new ExPerformanceCounter[]
				{
					exPerformanceCounter31
				});
				list.Add(this.PerfCounter_UNSUBSCRIBE_Total);
				this.PerfCounter_XPROXY_Failures = new ExPerformanceCounter(base.CategoryName, "XPROXY Failures", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_XPROXY_Failures, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_XPROXY_Failures);
				ExPerformanceCounter exPerformanceCounter32 = new ExPerformanceCounter(base.CategoryName, "XPROXY Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter32);
				this.PerfCounter_XPROXY_Total = new ExPerformanceCounter(base.CategoryName, "XPROXY Total", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_XPROXY_Total, new ExPerformanceCounter[]
				{
					exPerformanceCounter32
				});
				list.Add(this.PerfCounter_XPROXY_Total);
				this.PerfCounter_Average_Command_Processing_Time = new ExPerformanceCounter(base.CategoryName, "Average Command Processing Time (milliseconds)", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_Average_Command_Processing_Time, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_Average_Command_Processing_Time);
				ExPerformanceCounter exPerformanceCounter33 = new ExPerformanceCounter(base.CategoryName, "Connections Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter33);
				this.PerfCounter_Connections_Total = new ExPerformanceCounter(base.CategoryName, "Connections Total", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_Connections_Total, new ExPerformanceCounter[]
				{
					exPerformanceCounter33
				});
				list.Add(this.PerfCounter_Connections_Total);
				ExPerformanceCounter exPerformanceCounter34 = new ExPerformanceCounter(base.CategoryName, "SearchFolder Creation Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter34);
				this.PerfCounter_SEARCHFOLDER_CREATION_Total = new ExPerformanceCounter(base.CategoryName, "SearchFolder Creation Total", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_SEARCHFOLDER_CREATION_Total, new ExPerformanceCounter[]
				{
					exPerformanceCounter34
				});
				list.Add(this.PerfCounter_SEARCHFOLDER_CREATION_Total);
				ExPerformanceCounter exPerformanceCounter35 = new ExPerformanceCounter(base.CategoryName, "Folder View Reload Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter35);
				this.PerfCounter_ViewReload_Total = new ExPerformanceCounter(base.CategoryName, "Folder View Reload Total", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_ViewReload_Total, new ExPerformanceCounter[]
				{
					exPerformanceCounter35
				});
				list.Add(this.PerfCounter_ViewReload_Total);
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
				this.PerfCounter_ImapUidFix_Total = new ExPerformanceCounter(base.CategoryName, "Total IMAP UID Fixes", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_ImapUidFix_Total, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_ImapUidFix_Total);
				this.PerfCounter_ImapUidFix_Current = new ExPerformanceCounter(base.CategoryName, "Current IMAP UID Fixes", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_ImapUidFix_Current, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_ImapUidFix_Current);
				this.PerfCounter_ImapUidFix_Items_Total = new ExPerformanceCounter(base.CategoryName, "Total IMAP UID Items Fixed", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PerfCounter_ImapUidFix_Items_Total, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_ImapUidFix_Items_Total);
				long num = this.PerfCounter_Connections_Current.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter36 in list)
					{
						exPerformanceCounter36.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00016108 File Offset: 0x00014308
		internal Imap4CountersInstance(string instanceName) : base(instanceName, "MSExchangeImap4")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.PerfCounter_Connections_Current = new ExPerformanceCounter(base.CategoryName, "Current Connections", instanceName, null, new ExPerformanceCounter[0]);
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
				this.PerfCounter_APPEND_Failures = new ExPerformanceCounter(base.CategoryName, "Append Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_APPEND_Failures);
				ExPerformanceCounter exPerformanceCounter3 = new ExPerformanceCounter(base.CategoryName, "Append Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter3);
				this.PerfCounter_APPEND_Total = new ExPerformanceCounter(base.CategoryName, "Append Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter3
				});
				list.Add(this.PerfCounter_APPEND_Total);
				this.PerfCounter_AUTHENTICATE_Failures = new ExPerformanceCounter(base.CategoryName, "Authenticate Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_AUTHENTICATE_Failures);
				ExPerformanceCounter exPerformanceCounter4 = new ExPerformanceCounter(base.CategoryName, "Authenticate Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter4);
				this.PerfCounter_AUTHENTICATE_Total = new ExPerformanceCounter(base.CategoryName, "Authenticate Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter4
				});
				list.Add(this.PerfCounter_AUTHENTICATE_Total);
				this.PerfCounter_CAPABILITY_Failures = new ExPerformanceCounter(base.CategoryName, "Capability Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_CAPABILITY_Failures);
				ExPerformanceCounter exPerformanceCounter5 = new ExPerformanceCounter(base.CategoryName, "Capability Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter5);
				this.PerfCounter_CAPABILITY_Total = new ExPerformanceCounter(base.CategoryName, "Capability Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter5
				});
				list.Add(this.PerfCounter_CAPABILITY_Total);
				this.PerfCounter_CHECK_Failures = new ExPerformanceCounter(base.CategoryName, "Check Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_CHECK_Failures);
				ExPerformanceCounter exPerformanceCounter6 = new ExPerformanceCounter(base.CategoryName, "Check Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter6);
				this.PerfCounter_CHECK_Total = new ExPerformanceCounter(base.CategoryName, "Check Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter6
				});
				list.Add(this.PerfCounter_CHECK_Total);
				this.PerfCounter_CLOSE_Failures = new ExPerformanceCounter(base.CategoryName, "Close Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_CLOSE_Failures);
				ExPerformanceCounter exPerformanceCounter7 = new ExPerformanceCounter(base.CategoryName, "Close Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter7);
				this.PerfCounter_CLOSE_Total = new ExPerformanceCounter(base.CategoryName, "Close Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter7
				});
				list.Add(this.PerfCounter_CLOSE_Total);
				this.PerfCounter_COPY_Failures = new ExPerformanceCounter(base.CategoryName, "Copy Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_COPY_Failures);
				ExPerformanceCounter exPerformanceCounter8 = new ExPerformanceCounter(base.CategoryName, "Copy Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter8);
				this.PerfCounter_COPY_Total = new ExPerformanceCounter(base.CategoryName, "Copy Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter8
				});
				list.Add(this.PerfCounter_COPY_Total);
				this.PerfCounter_CREATE_Failures = new ExPerformanceCounter(base.CategoryName, "Create Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_CREATE_Failures);
				ExPerformanceCounter exPerformanceCounter9 = new ExPerformanceCounter(base.CategoryName, "Create Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter9);
				this.PerfCounter_CREATE_Total = new ExPerformanceCounter(base.CategoryName, "Create Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter9
				});
				list.Add(this.PerfCounter_CREATE_Total);
				this.PerfCounter_DELETE_Failures = new ExPerformanceCounter(base.CategoryName, "Delete Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_DELETE_Failures);
				ExPerformanceCounter exPerformanceCounter10 = new ExPerformanceCounter(base.CategoryName, "Delete Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter10);
				this.PerfCounter_DELETE_Total = new ExPerformanceCounter(base.CategoryName, "Delete Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter10
				});
				list.Add(this.PerfCounter_DELETE_Total);
				this.PerfCounter_EXAMINE_Failures = new ExPerformanceCounter(base.CategoryName, "Examine Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_EXAMINE_Failures);
				ExPerformanceCounter exPerformanceCounter11 = new ExPerformanceCounter(base.CategoryName, "Examine Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter11);
				this.PerfCounter_EXAMINE_Total = new ExPerformanceCounter(base.CategoryName, "Examine Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter11
				});
				list.Add(this.PerfCounter_EXAMINE_Total);
				this.PerfCounter_EXPUNGE_Failures = new ExPerformanceCounter(base.CategoryName, "Expunge Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_EXPUNGE_Failures);
				ExPerformanceCounter exPerformanceCounter12 = new ExPerformanceCounter(base.CategoryName, "Expunge Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter12);
				this.PerfCounter_EXPUNGE_Total = new ExPerformanceCounter(base.CategoryName, "Expunge Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter12
				});
				list.Add(this.PerfCounter_EXPUNGE_Total);
				this.PerfCounter_FETCH_Failures = new ExPerformanceCounter(base.CategoryName, "Fetch Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_FETCH_Failures);
				ExPerformanceCounter exPerformanceCounter13 = new ExPerformanceCounter(base.CategoryName, "Fetch Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter13);
				this.PerfCounter_FETCH_Total = new ExPerformanceCounter(base.CategoryName, "Fetch Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter13
				});
				list.Add(this.PerfCounter_FETCH_Total);
				this.PerfCounter_IDLE_Failures = new ExPerformanceCounter(base.CategoryName, "Idle Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_IDLE_Failures);
				ExPerformanceCounter exPerformanceCounter14 = new ExPerformanceCounter(base.CategoryName, "Idle Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter14);
				this.PerfCounter_IDLE_Total = new ExPerformanceCounter(base.CategoryName, "Idle Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter14
				});
				list.Add(this.PerfCounter_IDLE_Total);
				this.PerfCounter_LIST_Failures = new ExPerformanceCounter(base.CategoryName, "List Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_LIST_Failures);
				ExPerformanceCounter exPerformanceCounter15 = new ExPerformanceCounter(base.CategoryName, "List Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter15);
				this.PerfCounter_LIST_Total = new ExPerformanceCounter(base.CategoryName, "List Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter15
				});
				list.Add(this.PerfCounter_LIST_Total);
				this.PerfCounter_LOGIN_Failures = new ExPerformanceCounter(base.CategoryName, "Login Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_LOGIN_Failures);
				ExPerformanceCounter exPerformanceCounter16 = new ExPerformanceCounter(base.CategoryName, "Login Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter16);
				this.PerfCounter_LOGIN_Total = new ExPerformanceCounter(base.CategoryName, "Login Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter16
				});
				list.Add(this.PerfCounter_LOGIN_Total);
				this.PerfCounter_LOGOUT_Failures = new ExPerformanceCounter(base.CategoryName, "Logout Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_LOGOUT_Failures);
				ExPerformanceCounter exPerformanceCounter17 = new ExPerformanceCounter(base.CategoryName, "Logout Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter17);
				this.PerfCounter_LOGOUT_Total = new ExPerformanceCounter(base.CategoryName, "Logout Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter17
				});
				list.Add(this.PerfCounter_LOGOUT_Total);
				this.PerfCounter_LSUB_Failures = new ExPerformanceCounter(base.CategoryName, "LSUB Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_LSUB_Failures);
				ExPerformanceCounter exPerformanceCounter18 = new ExPerformanceCounter(base.CategoryName, "LSUB Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter18);
				this.PerfCounter_LSUB_Total = new ExPerformanceCounter(base.CategoryName, "LSUB Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter18
				});
				list.Add(this.PerfCounter_LSUB_Total);
				this.PerfCounter_MOVE_Failures = new ExPerformanceCounter(base.CategoryName, "Move Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_MOVE_Failures);
				ExPerformanceCounter exPerformanceCounter19 = new ExPerformanceCounter(base.CategoryName, "Move Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter19);
				this.PerfCounter_MOVE_Total = new ExPerformanceCounter(base.CategoryName, "Move Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter19
				});
				list.Add(this.PerfCounter_MOVE_Total);
				this.PerfCounter_ID_Failures = new ExPerformanceCounter(base.CategoryName, "ID Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_ID_Failures);
				ExPerformanceCounter exPerformanceCounter20 = new ExPerformanceCounter(base.CategoryName, "ID Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter20);
				this.PerfCounter_ID_Total = new ExPerformanceCounter(base.CategoryName, "ID Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter20
				});
				list.Add(this.PerfCounter_ID_Total);
				this.PerfCounter_NAMESPACE_Failures = new ExPerformanceCounter(base.CategoryName, "Namespace Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_NAMESPACE_Failures);
				ExPerformanceCounter exPerformanceCounter21 = new ExPerformanceCounter(base.CategoryName, "Namespace Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter21);
				this.PerfCounter_NAMESPACE_Total = new ExPerformanceCounter(base.CategoryName, "Namespace Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter21
				});
				list.Add(this.PerfCounter_NAMESPACE_Total);
				this.PerfCounter_NOOP_Failures = new ExPerformanceCounter(base.CategoryName, "NOOP Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_NOOP_Failures);
				ExPerformanceCounter exPerformanceCounter22 = new ExPerformanceCounter(base.CategoryName, "NOOP Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter22);
				this.PerfCounter_NOOP_Total = new ExPerformanceCounter(base.CategoryName, "NOOP Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter22
				});
				list.Add(this.PerfCounter_NOOP_Total);
				this.PerfCounter_RENAME_Failures = new ExPerformanceCounter(base.CategoryName, "Rename Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_RENAME_Failures);
				ExPerformanceCounter exPerformanceCounter23 = new ExPerformanceCounter(base.CategoryName, "Rename Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter23);
				this.PerfCounter_RENAME_Total = new ExPerformanceCounter(base.CategoryName, "Rename Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter23
				});
				list.Add(this.PerfCounter_RENAME_Total);
				this.PerfCounter_Request_Failures = new ExPerformanceCounter(base.CategoryName, "Request Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_Request_Failures);
				ExPerformanceCounter exPerformanceCounter24 = new ExPerformanceCounter(base.CategoryName, "Request Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter24);
				this.PerfCounter_Request_Total = new ExPerformanceCounter(base.CategoryName, "Request Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter24
				});
				list.Add(this.PerfCounter_Request_Total);
				this.PerfCounter_SEARCH_Failures = new ExPerformanceCounter(base.CategoryName, "Search Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_SEARCH_Failures);
				ExPerformanceCounter exPerformanceCounter25 = new ExPerformanceCounter(base.CategoryName, "Search Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter25);
				this.PerfCounter_SEARCH_Total = new ExPerformanceCounter(base.CategoryName, "Search Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter25
				});
				list.Add(this.PerfCounter_SEARCH_Total);
				this.PerfCounter_SELECT_Failures = new ExPerformanceCounter(base.CategoryName, "Select Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_SELECT_Failures);
				ExPerformanceCounter exPerformanceCounter26 = new ExPerformanceCounter(base.CategoryName, "Select Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter26);
				this.PerfCounter_SELECT_Total = new ExPerformanceCounter(base.CategoryName, "Select Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter26
				});
				list.Add(this.PerfCounter_SELECT_Total);
				this.PerfCounter_STARTTLS_Failures = new ExPerformanceCounter(base.CategoryName, "STARTTLS Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_STARTTLS_Failures);
				ExPerformanceCounter exPerformanceCounter27 = new ExPerformanceCounter(base.CategoryName, "STARTTLS Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter27);
				this.PerfCounter_STARTTLS_Total = new ExPerformanceCounter(base.CategoryName, "STARTTLS Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter27
				});
				list.Add(this.PerfCounter_STARTTLS_Total);
				this.PerfCounter_STATUS_Failures = new ExPerformanceCounter(base.CategoryName, "Status Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_STATUS_Failures);
				ExPerformanceCounter exPerformanceCounter28 = new ExPerformanceCounter(base.CategoryName, "Status Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter28);
				this.PerfCounter_STATUS_Total = new ExPerformanceCounter(base.CategoryName, "Status Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter28
				});
				list.Add(this.PerfCounter_STATUS_Total);
				this.PerfCounter_STORE_Failures = new ExPerformanceCounter(base.CategoryName, "Store Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_STORE_Failures);
				ExPerformanceCounter exPerformanceCounter29 = new ExPerformanceCounter(base.CategoryName, "Store Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter29);
				this.PerfCounter_STORE_Total = new ExPerformanceCounter(base.CategoryName, "Store Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter29
				});
				list.Add(this.PerfCounter_STORE_Total);
				this.PerfCounter_SUBSCRIBE_Failures = new ExPerformanceCounter(base.CategoryName, "Subscribe Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_SUBSCRIBE_Failures);
				ExPerformanceCounter exPerformanceCounter30 = new ExPerformanceCounter(base.CategoryName, "Subscribe Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter30);
				this.PerfCounter_SUBSCRIBE_Total = new ExPerformanceCounter(base.CategoryName, "Subscribe Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter30
				});
				list.Add(this.PerfCounter_SUBSCRIBE_Total);
				this.PerfCounter_UNSUBSCRIBE_Failures = new ExPerformanceCounter(base.CategoryName, "Unsubscribe Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_UNSUBSCRIBE_Failures);
				ExPerformanceCounter exPerformanceCounter31 = new ExPerformanceCounter(base.CategoryName, "Unsubscribe Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter31);
				this.PerfCounter_UNSUBSCRIBE_Total = new ExPerformanceCounter(base.CategoryName, "Unsubscribe Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter31
				});
				list.Add(this.PerfCounter_UNSUBSCRIBE_Total);
				this.PerfCounter_XPROXY_Failures = new ExPerformanceCounter(base.CategoryName, "XPROXY Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_XPROXY_Failures);
				ExPerformanceCounter exPerformanceCounter32 = new ExPerformanceCounter(base.CategoryName, "XPROXY Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter32);
				this.PerfCounter_XPROXY_Total = new ExPerformanceCounter(base.CategoryName, "XPROXY Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter32
				});
				list.Add(this.PerfCounter_XPROXY_Total);
				this.PerfCounter_Average_Command_Processing_Time = new ExPerformanceCounter(base.CategoryName, "Average Command Processing Time (milliseconds)", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_Average_Command_Processing_Time);
				ExPerformanceCounter exPerformanceCounter33 = new ExPerformanceCounter(base.CategoryName, "Connections Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter33);
				this.PerfCounter_Connections_Total = new ExPerformanceCounter(base.CategoryName, "Connections Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter33
				});
				list.Add(this.PerfCounter_Connections_Total);
				ExPerformanceCounter exPerformanceCounter34 = new ExPerformanceCounter(base.CategoryName, "SearchFolder Creation Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter34);
				this.PerfCounter_SEARCHFOLDER_CREATION_Total = new ExPerformanceCounter(base.CategoryName, "SearchFolder Creation Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter34
				});
				list.Add(this.PerfCounter_SEARCHFOLDER_CREATION_Total);
				ExPerformanceCounter exPerformanceCounter35 = new ExPerformanceCounter(base.CategoryName, "Folder View Reload Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter35);
				this.PerfCounter_ViewReload_Total = new ExPerformanceCounter(base.CategoryName, "Folder View Reload Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter35
				});
				list.Add(this.PerfCounter_ViewReload_Total);
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
				this.PerfCounter_ImapUidFix_Total = new ExPerformanceCounter(base.CategoryName, "Total IMAP UID Fixes", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_ImapUidFix_Total);
				this.PerfCounter_ImapUidFix_Current = new ExPerformanceCounter(base.CategoryName, "Current IMAP UID Fixes", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_ImapUidFix_Current);
				this.PerfCounter_ImapUidFix_Items_Total = new ExPerformanceCounter(base.CategoryName, "Total IMAP UID Items Fixed", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PerfCounter_ImapUidFix_Items_Total);
				long num = this.PerfCounter_Connections_Current.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter36 in list)
					{
						exPerformanceCounter36.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0001763C File Offset: 0x0001583C
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

		// Token: 0x04000223 RID: 547
		public readonly ExPerformanceCounter PerfCounter_Connections_Current;

		// Token: 0x04000224 RID: 548
		public readonly ExPerformanceCounter PerfCounter_Connections_Failed;

		// Token: 0x04000225 RID: 549
		public readonly ExPerformanceCounter PerfCounter_Connections_Rejected;

		// Token: 0x04000226 RID: 550
		public readonly ExPerformanceCounter PerfCounter_Connections_Total;

		// Token: 0x04000227 RID: 551
		public readonly ExPerformanceCounter PerfCounter_UnAuth_Connections_Current;

		// Token: 0x04000228 RID: 552
		public readonly ExPerformanceCounter PerfCounter_Proxy_Connections_Current;

		// Token: 0x04000229 RID: 553
		public readonly ExPerformanceCounter PerfCounter_Proxy_Connections_Failed;

		// Token: 0x0400022A RID: 554
		public readonly ExPerformanceCounter PerfCounter_Proxy_Connections_Total;

		// Token: 0x0400022B RID: 555
		public readonly ExPerformanceCounter PerfCounter_SSLConnections_Current;

		// Token: 0x0400022C RID: 556
		public readonly ExPerformanceCounter PerfCounter_SSLConnections_Total;

		// Token: 0x0400022D RID: 557
		public readonly ExPerformanceCounter PerfCounter_Invalid_Commands;

		// Token: 0x0400022E RID: 558
		public readonly ExPerformanceCounter PerfCounter_APPEND_Failures;

		// Token: 0x0400022F RID: 559
		public readonly ExPerformanceCounter PerfCounter_APPEND_Total;

		// Token: 0x04000230 RID: 560
		public readonly ExPerformanceCounter PerfCounter_AUTHENTICATE_Failures;

		// Token: 0x04000231 RID: 561
		public readonly ExPerformanceCounter PerfCounter_AUTHENTICATE_Total;

		// Token: 0x04000232 RID: 562
		public readonly ExPerformanceCounter PerfCounter_CAPABILITY_Failures;

		// Token: 0x04000233 RID: 563
		public readonly ExPerformanceCounter PerfCounter_CAPABILITY_Total;

		// Token: 0x04000234 RID: 564
		public readonly ExPerformanceCounter PerfCounter_CHECK_Failures;

		// Token: 0x04000235 RID: 565
		public readonly ExPerformanceCounter PerfCounter_CHECK_Total;

		// Token: 0x04000236 RID: 566
		public readonly ExPerformanceCounter PerfCounter_CLOSE_Failures;

		// Token: 0x04000237 RID: 567
		public readonly ExPerformanceCounter PerfCounter_CLOSE_Total;

		// Token: 0x04000238 RID: 568
		public readonly ExPerformanceCounter PerfCounter_COPY_Failures;

		// Token: 0x04000239 RID: 569
		public readonly ExPerformanceCounter PerfCounter_COPY_Total;

		// Token: 0x0400023A RID: 570
		public readonly ExPerformanceCounter PerfCounter_CREATE_Failures;

		// Token: 0x0400023B RID: 571
		public readonly ExPerformanceCounter PerfCounter_CREATE_Total;

		// Token: 0x0400023C RID: 572
		public readonly ExPerformanceCounter PerfCounter_DELETE_Failures;

		// Token: 0x0400023D RID: 573
		public readonly ExPerformanceCounter PerfCounter_DELETE_Total;

		// Token: 0x0400023E RID: 574
		public readonly ExPerformanceCounter PerfCounter_EXAMINE_Failures;

		// Token: 0x0400023F RID: 575
		public readonly ExPerformanceCounter PerfCounter_EXAMINE_Total;

		// Token: 0x04000240 RID: 576
		public readonly ExPerformanceCounter PerfCounter_EXPUNGE_Failures;

		// Token: 0x04000241 RID: 577
		public readonly ExPerformanceCounter PerfCounter_EXPUNGE_Total;

		// Token: 0x04000242 RID: 578
		public readonly ExPerformanceCounter PerfCounter_FETCH_Failures;

		// Token: 0x04000243 RID: 579
		public readonly ExPerformanceCounter PerfCounter_FETCH_Total;

		// Token: 0x04000244 RID: 580
		public readonly ExPerformanceCounter PerfCounter_IDLE_Failures;

		// Token: 0x04000245 RID: 581
		public readonly ExPerformanceCounter PerfCounter_IDLE_Total;

		// Token: 0x04000246 RID: 582
		public readonly ExPerformanceCounter PerfCounter_LIST_Failures;

		// Token: 0x04000247 RID: 583
		public readonly ExPerformanceCounter PerfCounter_LIST_Total;

		// Token: 0x04000248 RID: 584
		public readonly ExPerformanceCounter PerfCounter_LOGIN_Failures;

		// Token: 0x04000249 RID: 585
		public readonly ExPerformanceCounter PerfCounter_LOGIN_Total;

		// Token: 0x0400024A RID: 586
		public readonly ExPerformanceCounter PerfCounter_LOGOUT_Failures;

		// Token: 0x0400024B RID: 587
		public readonly ExPerformanceCounter PerfCounter_LOGOUT_Total;

		// Token: 0x0400024C RID: 588
		public readonly ExPerformanceCounter PerfCounter_LSUB_Failures;

		// Token: 0x0400024D RID: 589
		public readonly ExPerformanceCounter PerfCounter_LSUB_Total;

		// Token: 0x0400024E RID: 590
		public readonly ExPerformanceCounter PerfCounter_MOVE_Failures;

		// Token: 0x0400024F RID: 591
		public readonly ExPerformanceCounter PerfCounter_MOVE_Total;

		// Token: 0x04000250 RID: 592
		public readonly ExPerformanceCounter PerfCounter_ID_Failures;

		// Token: 0x04000251 RID: 593
		public readonly ExPerformanceCounter PerfCounter_ID_Total;

		// Token: 0x04000252 RID: 594
		public readonly ExPerformanceCounter PerfCounter_NAMESPACE_Failures;

		// Token: 0x04000253 RID: 595
		public readonly ExPerformanceCounter PerfCounter_NAMESPACE_Total;

		// Token: 0x04000254 RID: 596
		public readonly ExPerformanceCounter PerfCounter_NOOP_Failures;

		// Token: 0x04000255 RID: 597
		public readonly ExPerformanceCounter PerfCounter_NOOP_Total;

		// Token: 0x04000256 RID: 598
		public readonly ExPerformanceCounter PerfCounter_RENAME_Failures;

		// Token: 0x04000257 RID: 599
		public readonly ExPerformanceCounter PerfCounter_RENAME_Total;

		// Token: 0x04000258 RID: 600
		public readonly ExPerformanceCounter PerfCounter_Request_Failures;

		// Token: 0x04000259 RID: 601
		public readonly ExPerformanceCounter PerfCounter_Request_Total;

		// Token: 0x0400025A RID: 602
		public readonly ExPerformanceCounter PerfCounter_SEARCH_Failures;

		// Token: 0x0400025B RID: 603
		public readonly ExPerformanceCounter PerfCounter_SEARCH_Total;

		// Token: 0x0400025C RID: 604
		public readonly ExPerformanceCounter PerfCounter_SELECT_Failures;

		// Token: 0x0400025D RID: 605
		public readonly ExPerformanceCounter PerfCounter_SELECT_Total;

		// Token: 0x0400025E RID: 606
		public readonly ExPerformanceCounter PerfCounter_STARTTLS_Failures;

		// Token: 0x0400025F RID: 607
		public readonly ExPerformanceCounter PerfCounter_STARTTLS_Total;

		// Token: 0x04000260 RID: 608
		public readonly ExPerformanceCounter PerfCounter_STATUS_Failures;

		// Token: 0x04000261 RID: 609
		public readonly ExPerformanceCounter PerfCounter_STATUS_Total;

		// Token: 0x04000262 RID: 610
		public readonly ExPerformanceCounter PerfCounter_STORE_Failures;

		// Token: 0x04000263 RID: 611
		public readonly ExPerformanceCounter PerfCounter_STORE_Total;

		// Token: 0x04000264 RID: 612
		public readonly ExPerformanceCounter PerfCounter_SUBSCRIBE_Failures;

		// Token: 0x04000265 RID: 613
		public readonly ExPerformanceCounter PerfCounter_SUBSCRIBE_Total;

		// Token: 0x04000266 RID: 614
		public readonly ExPerformanceCounter PerfCounter_UNSUBSCRIBE_Failures;

		// Token: 0x04000267 RID: 615
		public readonly ExPerformanceCounter PerfCounter_UNSUBSCRIBE_Total;

		// Token: 0x04000268 RID: 616
		public readonly ExPerformanceCounter PerfCounter_XPROXY_Failures;

		// Token: 0x04000269 RID: 617
		public readonly ExPerformanceCounter PerfCounter_XPROXY_Total;

		// Token: 0x0400026A RID: 618
		public readonly ExPerformanceCounter PerfCounter_Average_Command_Processing_Time;

		// Token: 0x0400026B RID: 619
		public readonly ExPerformanceCounter PerfCounter_SEARCHFOLDER_CREATION_Total;

		// Token: 0x0400026C RID: 620
		public readonly ExPerformanceCounter PerfCounter_ViewReload_Total;

		// Token: 0x0400026D RID: 621
		public readonly ExPerformanceCounter PerfCounter_RatePerMinuteOfTransientMailboxConnectionFailures;

		// Token: 0x0400026E RID: 622
		public readonly ExPerformanceCounter PerfCounter_RatePerMinuteOfMailboxOfflineErrors;

		// Token: 0x0400026F RID: 623
		public readonly ExPerformanceCounter PerfCounter_RatePerMinuteOfTransientStorageErrors;

		// Token: 0x04000270 RID: 624
		public readonly ExPerformanceCounter PerfCounter_RatePerMinuteOfPermanentStorageErrors;

		// Token: 0x04000271 RID: 625
		public readonly ExPerformanceCounter PerfCounter_RatePerMinuteOfTransientActiveDirectoryErrors;

		// Token: 0x04000272 RID: 626
		public readonly ExPerformanceCounter PerfCounter_RatePerMinuteOfPermanentActiveDirectoryErrors;

		// Token: 0x04000273 RID: 627
		public readonly ExPerformanceCounter PerfCounter_RatePerMinuteOfTransientErrors;

		// Token: 0x04000274 RID: 628
		public readonly ExPerformanceCounter PerfCounter_AverageRpcLatency;

		// Token: 0x04000275 RID: 629
		public readonly ExPerformanceCounter PerfCounter_AverageLdapLatency;

		// Token: 0x04000276 RID: 630
		public readonly ExPerformanceCounter PerfCounter_ImapUidFix_Total;

		// Token: 0x04000277 RID: 631
		public readonly ExPerformanceCounter PerfCounter_ImapUidFix_Current;

		// Token: 0x04000278 RID: 632
		public readonly ExPerformanceCounter PerfCounter_ImapUidFix_Items_Total;
	}
}
