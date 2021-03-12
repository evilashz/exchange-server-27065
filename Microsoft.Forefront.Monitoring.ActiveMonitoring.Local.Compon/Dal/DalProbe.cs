using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Hygiene.Data;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Dal
{
	// Token: 0x0200005E RID: 94
	public class DalProbe : ProbeWorkItem
	{
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000F49C File Offset: 0x0000D69C
		private DalWorkDefinition DalWork
		{
			get
			{
				if (this.dalWork == null)
				{
					XmlSerializer xmlSerializer = new XmlSerializer(typeof(DalWorkDefinition), new XmlRootAttribute("WorkContext"));
					XmlReader xmlReader = XmlReader.Create(new StringReader(base.Definition.ExtensionAttributes));
					xmlReader.ReadStartElement();
					xmlReader.MoveToContent();
					this.dalWork = (DalWorkDefinition)xmlSerializer.Deserialize(xmlReader);
				}
				return this.dalWork;
			}
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000F508 File Offset: 0x0000D708
		public static XElement Serialize(IConfigurable configObj)
		{
			Type type = configObj.GetType();
			configObj = new TenantSettingFacade<HostedSpamFilterConfig>(configObj);
			IPropertyBag propertyBag = configObj as IPropertyBag;
			XElement xelement = new XElement(XName.Get(type.Name));
			PropertyDefinition[] array = DalHelper.GetPropertyDefinitions(configObj, true).ToArray<PropertyDefinition>();
			foreach (PropertyDefinition propertyDefinition in array)
			{
				object obj = propertyBag[propertyDefinition];
				if (obj != null)
				{
					xelement.Add(new XElement(XName.Get(propertyDefinition.Name))
					{
						Value = obj.ToString()
					});
				}
			}
			return xelement;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000F5A0 File Offset: 0x0000D7A0
		public void Execute(string xml)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(DalWorkDefinition));
			this.dalWork = (DalWorkDefinition)xmlSerializer.Deserialize(new StringReader(xml));
			this.DoWork(CancellationToken.None);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000F5E0 File Offset: 0x0000D7E0
		protected override void DoWork(CancellationToken cancellationToken)
		{
			try
			{
				this.WriteResult("DalProbe started. ", new object[0]);
				Dictionary<string, object> variables = new Dictionary<string, object>();
				foreach (DalProbeOperation dalProbeOperation in this.DalWork.Operations)
				{
					this.WriteResult("Executing operation: {0}", new object[]
					{
						dalProbeOperation
					});
					if (cancellationToken.IsCancellationRequested)
					{
						this.WriteResult("Cancellation Requested", new object[0]);
						break;
					}
					dalProbeOperation.Execute(variables);
				}
				this.WriteResult("DalProbe finished.", new object[0]);
			}
			catch (Exception ex)
			{
				this.WriteResult("Exception: {0} ", new object[]
				{
					ex
				});
				throw;
			}
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000F6A4 File Offset: 0x0000D8A4
		private void WriteResult(string format, params object[] objs)
		{
			ProbeResult result = base.Result;
			result.ExecutionContext += string.Format(format, objs);
			ProbeResult result2 = base.Result;
			result2.ExecutionContext += "|";
		}

		// Token: 0x04000186 RID: 390
		private DalWorkDefinition dalWork;
	}
}
