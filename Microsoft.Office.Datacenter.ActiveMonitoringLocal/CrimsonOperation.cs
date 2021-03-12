using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000070 RID: 112
	internal abstract class CrimsonOperation<T> : IDisposable where T : IPersistence, new()
	{
		// Token: 0x06000662 RID: 1634 RVA: 0x0001B0B7 File Offset: 0x000192B7
		internal CrimsonOperation() : this(null, null)
		{
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0001B0C1 File Offset: 0x000192C1
		internal CrimsonOperation(EventBookmark bookmark) : this(bookmark, null)
		{
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x0001B0CB File Offset: 0x000192CB
		internal CrimsonOperation(EventBookmark bookmark, string channelName)
		{
			if (channelName == null)
			{
				channelName = CrimsonHelper.GetChannelName<T>();
			}
			this.ChannelName = channelName;
			this.BookMark = bookmark;
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000665 RID: 1637 RVA: 0x0001B0F6 File Offset: 0x000192F6
		// (set) Token: 0x06000666 RID: 1638 RVA: 0x0001B0FE File Offset: 0x000192FE
		internal bool IsAccessPropertyDirectly { get; set; }

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000667 RID: 1639 RVA: 0x0001B107 File Offset: 0x00019307
		// (set) Token: 0x06000668 RID: 1640 RVA: 0x0001B10F File Offset: 0x0001930F
		internal string ExplicitXPathQuery { get; set; }

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x0001B118 File Offset: 0x00019318
		// (set) Token: 0x0600066A RID: 1642 RVA: 0x0001B120 File Offset: 0x00019320
		internal DateTime? QueryStartTime { get; set; }

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x0001B129 File Offset: 0x00019329
		// (set) Token: 0x0600066C RID: 1644 RVA: 0x0001B131 File Offset: 0x00019331
		internal DateTime? QueryEndTime { get; set; }

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x0600066D RID: 1645 RVA: 0x0001B13A File Offset: 0x0001933A
		// (set) Token: 0x0600066E RID: 1646 RVA: 0x0001B142 File Offset: 0x00019342
		internal string QueryUserPropertyCondition { get; set; }

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x0001B14B File Offset: 0x0001934B
		// (set) Token: 0x06000670 RID: 1648 RVA: 0x0001B153 File Offset: 0x00019353
		internal CrimsonConnectionInfo ConnectionInfo { get; set; }

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x0001B15C File Offset: 0x0001935C
		// (set) Token: 0x06000672 RID: 1650 RVA: 0x0001B164 File Offset: 0x00019364
		protected string ChannelName { get; set; }

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x0001B16D File Offset: 0x0001936D
		// (set) Token: 0x06000674 RID: 1652 RVA: 0x0001B175 File Offset: 0x00019375
		protected EventBookmark BookMark { get; set; }

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000675 RID: 1653 RVA: 0x0001B17E File Offset: 0x0001937E
		// (set) Token: 0x06000676 RID: 1654 RVA: 0x0001B186 File Offset: 0x00019386
		protected bool IsInitialized { get; set; }

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x0001B18F File Offset: 0x0001938F
		// (set) Token: 0x06000678 RID: 1656 RVA: 0x0001B197 File Offset: 0x00019397
		private protected EventLogQuery LogQuery { protected get; private set; }

		// Token: 0x06000679 RID: 1657 RVA: 0x0001B1A0 File Offset: 0x000193A0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x0001B1B0 File Offset: 0x000193B0
		internal EventLogQuery GetQueryObject()
		{
			if (this.LogQuery != null)
			{
				return this.LogQuery;
			}
			string query;
			if (this.ExplicitXPathQuery != null)
			{
				query = this.ExplicitXPathQuery;
			}
			else
			{
				query = this.GetDefaultXPathQuery();
			}
			EventLogQuery eventLogQuery = new EventLogQuery(this.ChannelName, PathType.LogName, query);
			if (this.ConnectionInfo != null && this.ConnectionInfo.ComputerName != null)
			{
				if (this.ConnectionInfo.UserName != null)
				{
					eventLogQuery.Session = new EventLogSession(this.ConnectionInfo.ComputerName, this.ConnectionInfo.UserDomain, this.ConnectionInfo.UserName, this.ConnectionInfo.Password, SessionAuthentication.Default);
				}
				else
				{
					eventLogQuery.Session = new EventLogSession(this.ConnectionInfo.ComputerName);
				}
			}
			this.LogQuery = eventLogQuery;
			return this.LogQuery;
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0001B274 File Offset: 0x00019474
		internal T EventToObject(EventRecord record)
		{
			T result = default(T);
			if (record != null)
			{
				try
				{
					result = ((default(T) == null) ? Activator.CreateInstance<T>() : default(T));
					LocalDataAccessMetaData metaData = new LocalDataAccessMetaData(record);
					Dictionary<string, string> eventRecordProperties = this.GetEventRecordProperties(record);
					result.Initialize(eventRecordProperties, metaData);
				}
				catch (FormatException arg)
				{
					WTFDiagnostics.TraceError<FormatException>(WTFLog.DataAccess, this.traceContext, "[CrimsonOperation.EventToObject]: FormatException - Failed to initialize object properties: {0}", arg, null, "EventToObject", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\CrimsonOperation.cs", 214);
					result = default(T);
				}
				catch (EventLogException arg2)
				{
					WTFDiagnostics.TraceError<EventLogException>(WTFLog.DataAccess, this.traceContext, "[CrimsonOperation.EventToObject]: EventLogException - Failed to read Crimson event: {0}", arg2, null, "EventToObject", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\CrimsonOperation.cs", 221);
					result = default(T);
				}
				catch (ObjectDisposedException arg3)
				{
					WTFDiagnostics.TraceError<ObjectDisposedException>(WTFLog.DataAccess, this.traceContext, "[CrimsonOperation.EventToObject]:  ObjectDisposedException - Failed to read Crimson event: {0}", arg3, null, "EventToObject", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\CrimsonOperation.cs", 228);
					result = default(T);
				}
				catch (Exception arg4)
				{
					WTFDiagnostics.TraceError<Exception>(WTFLog.DataAccess, this.traceContext, "[CrimsonOperation.EventToObject]: Unknown Excption - Failed to read Crimson event: {0}", arg4, null, "EventToObject", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\CrimsonOperation.cs", 235);
					result = default(T);
				}
			}
			return result;
		}

		// Token: 0x0600067C RID: 1660
		internal abstract void Cleanup();

		// Token: 0x0600067D RID: 1661 RVA: 0x0001B3D8 File Offset: 0x000195D8
		internal Dictionary<string, string> GetEventRecordProperties(EventRecord record)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>(20);
			string xml = record.ToXml();
			SafeXmlDocument safeXmlDocument = new SafeXmlDocument();
			safeXmlDocument.LoadXml(xml);
			using (XmlNodeList elementsByTagName = safeXmlDocument.GetElementsByTagName("EventXML"))
			{
				if (elementsByTagName != null && elementsByTagName.Count > 0)
				{
					XmlNode xmlNode = elementsByTagName.Item(0);
					using (XmlNodeList childNodes = xmlNode.ChildNodes)
					{
						foreach (object obj in childNodes)
						{
							XmlNode xmlNode2 = (XmlNode)obj;
							dictionary.Add(xmlNode2.Name, xmlNode2.InnerText);
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0001B4BC File Offset: 0x000196BC
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					if (this.LogQuery != null && this.LogQuery.Session != null && this.LogQuery.Session != EventLogSession.GlobalSession)
					{
						this.LogQuery.Session.Dispose();
						this.LogQuery.Session = null;
					}
					this.Cleanup();
				}
				this.disposed = true;
			}
		}

		// Token: 0x0600067F RID: 1663
		protected abstract string GetDefaultXPathQuery();

		// Token: 0x04000431 RID: 1073
		private bool disposed;

		// Token: 0x04000432 RID: 1074
		private TracingContext traceContext = TracingContext.Default;
	}
}
