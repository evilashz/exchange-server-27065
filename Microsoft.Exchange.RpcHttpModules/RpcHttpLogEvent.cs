using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcHttpModules
{
	// Token: 0x02000013 RID: 19
	internal class RpcHttpLogEvent : ILogEvent
	{
		// Token: 0x06000060 RID: 96 RVA: 0x00003076 File Offset: 0x00001276
		public RpcHttpLogEvent(string stageProperty)
		{
			this.rpcHttpLogAttributes = new SortedDictionary<RpcHttpLogEvent.LoggingAttribute, string>();
			this.Stage = stageProperty;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00003090 File Offset: 0x00001290
		public string EventId
		{
			get
			{
				return "RpcHttp";
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00003097 File Offset: 0x00001297
		// (set) Token: 0x06000063 RID: 99 RVA: 0x000030A0 File Offset: 0x000012A0
		public string Stage
		{
			get
			{
				return this.GetAttribute(RpcHttpLogEvent.LoggingAttribute.Stage);
			}
			set
			{
				this.SetAttribute(RpcHttpLogEvent.LoggingAttribute.Stage, value);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000064 RID: 100 RVA: 0x000030AA File Offset: 0x000012AA
		// (set) Token: 0x06000065 RID: 101 RVA: 0x000030B3 File Offset: 0x000012B3
		public string UserName
		{
			get
			{
				return this.GetAttribute(RpcHttpLogEvent.LoggingAttribute.UserName);
			}
			set
			{
				this.SetAttribute(RpcHttpLogEvent.LoggingAttribute.UserName, value);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000066 RID: 102 RVA: 0x000030BD File Offset: 0x000012BD
		// (set) Token: 0x06000067 RID: 103 RVA: 0x000030C6 File Offset: 0x000012C6
		public string OutlookSessionId
		{
			get
			{
				return this.GetAttribute(RpcHttpLogEvent.LoggingAttribute.OutlookSessionId);
			}
			set
			{
				this.SetAttribute(RpcHttpLogEvent.LoggingAttribute.OutlookSessionId, value);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000030D0 File Offset: 0x000012D0
		// (set) Token: 0x06000069 RID: 105 RVA: 0x000030D9 File Offset: 0x000012D9
		public string AuthType
		{
			get
			{
				return this.GetAttribute(RpcHttpLogEvent.LoggingAttribute.AuthType);
			}
			set
			{
				this.SetAttribute(RpcHttpLogEvent.LoggingAttribute.AuthType, value);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600006A RID: 106 RVA: 0x000030E3 File Offset: 0x000012E3
		// (set) Token: 0x0600006B RID: 107 RVA: 0x000030EC File Offset: 0x000012EC
		public string Status
		{
			get
			{
				return this.GetAttribute(RpcHttpLogEvent.LoggingAttribute.Status);
			}
			set
			{
				this.SetAttribute(RpcHttpLogEvent.LoggingAttribute.Status, value);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600006C RID: 108 RVA: 0x000030F6 File Offset: 0x000012F6
		// (set) Token: 0x0600006D RID: 109 RVA: 0x000030FF File Offset: 0x000012FF
		public string HttpVerb
		{
			get
			{
				return this.GetAttribute(RpcHttpLogEvent.LoggingAttribute.HttpVerb);
			}
			set
			{
				this.SetAttribute(RpcHttpLogEvent.LoggingAttribute.HttpVerb, value);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00003109 File Offset: 0x00001309
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00003112 File Offset: 0x00001312
		public string UriQueryString
		{
			get
			{
				return this.GetAttribute(RpcHttpLogEvent.LoggingAttribute.UriQueryString);
			}
			set
			{
				this.SetAttribute(RpcHttpLogEvent.LoggingAttribute.UriQueryString, value);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000070 RID: 112 RVA: 0x0000311C File Offset: 0x0000131C
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00003125 File Offset: 0x00001325
		public string RpcHttpUserName
		{
			get
			{
				return this.GetAttribute(RpcHttpLogEvent.LoggingAttribute.RpcHttpUserName);
			}
			set
			{
				this.SetAttribute(RpcHttpLogEvent.LoggingAttribute.RpcHttpUserName, value);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000072 RID: 114 RVA: 0x0000312F File Offset: 0x0000132F
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00003138 File Offset: 0x00001338
		public string ServerTarget
		{
			get
			{
				return this.GetAttribute(RpcHttpLogEvent.LoggingAttribute.ServerTarget);
			}
			set
			{
				this.SetAttribute(RpcHttpLogEvent.LoggingAttribute.ServerTarget, value);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00003142 File Offset: 0x00001342
		// (set) Token: 0x06000075 RID: 117 RVA: 0x0000314C File Offset: 0x0000134C
		public string FEServer
		{
			get
			{
				return this.GetAttribute(RpcHttpLogEvent.LoggingAttribute.FEServer);
			}
			set
			{
				this.SetAttribute(RpcHttpLogEvent.LoggingAttribute.FEServer, value);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00003157 File Offset: 0x00001357
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00003161 File Offset: 0x00001361
		public string RequestId
		{
			get
			{
				return this.GetAttribute(RpcHttpLogEvent.LoggingAttribute.RequestId);
			}
			set
			{
				this.SetAttribute(RpcHttpLogEvent.LoggingAttribute.RequestId, value);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000078 RID: 120 RVA: 0x0000316C File Offset: 0x0000136C
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00003176 File Offset: 0x00001376
		public string AssociationGuid
		{
			get
			{
				return this.GetAttribute(RpcHttpLogEvent.LoggingAttribute.AssociationGuid);
			}
			set
			{
				this.SetAttribute(RpcHttpLogEvent.LoggingAttribute.AssociationGuid, value);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00003181 File Offset: 0x00001381
		// (set) Token: 0x0600007B RID: 123 RVA: 0x0000318B File Offset: 0x0000138B
		public string ClientIp
		{
			get
			{
				return this.GetAttribute(RpcHttpLogEvent.LoggingAttribute.ClientIp);
			}
			set
			{
				this.SetAttribute(RpcHttpLogEvent.LoggingAttribute.ClientIp, value);
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003198 File Offset: 0x00001398
		public ICollection<KeyValuePair<string, object>> GetEventData()
		{
			KeyValuePair<string, object>[] array = new KeyValuePair<string, object>[this.rpcHttpLogAttributes.Keys.Count];
			int num = 0;
			foreach (KeyValuePair<RpcHttpLogEvent.LoggingAttribute, string> keyValuePair in this.rpcHttpLogAttributes)
			{
				array[num++] = new KeyValuePair<string, object>(keyValuePair.Key.ToString(), keyValuePair.Value);
			}
			return array;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000322C File Offset: 0x0000142C
		private string GetAttribute(RpcHttpLogEvent.LoggingAttribute attribute)
		{
			string result = null;
			if (this.rpcHttpLogAttributes.TryGetValue(attribute, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000324E File Offset: 0x0000144E
		private void SetAttribute(RpcHttpLogEvent.LoggingAttribute attribute, string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				this.rpcHttpLogAttributes[attribute] = value;
			}
		}

		// Token: 0x0400002B RID: 43
		private readonly SortedDictionary<RpcHttpLogEvent.LoggingAttribute, string> rpcHttpLogAttributes;

		// Token: 0x02000014 RID: 20
		internal enum LoggingAttribute
		{
			// Token: 0x0400002D RID: 45
			Stage,
			// Token: 0x0400002E RID: 46
			UserName,
			// Token: 0x0400002F RID: 47
			OutlookSessionId,
			// Token: 0x04000030 RID: 48
			AuthType,
			// Token: 0x04000031 RID: 49
			Status,
			// Token: 0x04000032 RID: 50
			HttpVerb,
			// Token: 0x04000033 RID: 51
			UriQueryString,
			// Token: 0x04000034 RID: 52
			RpcHttpUserName,
			// Token: 0x04000035 RID: 53
			ServerTarget,
			// Token: 0x04000036 RID: 54
			FEServer,
			// Token: 0x04000037 RID: 55
			RequestId,
			// Token: 0x04000038 RID: 56
			AssociationGuid,
			// Token: 0x04000039 RID: 57
			ClientIp
		}
	}
}
