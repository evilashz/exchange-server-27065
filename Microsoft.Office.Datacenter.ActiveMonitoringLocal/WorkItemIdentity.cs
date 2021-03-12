using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000007 RID: 7
	public abstract class WorkItemIdentity
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00004C48 File Offset: 0x00002E48
		protected WorkItemIdentity(Component component, string localName, string targetResource)
		{
			ArgumentValidator.ThrowIfNull("component", component);
			ArgumentValidator.ThrowIfNullOrEmpty("localName", localName);
			this.Component = component;
			this.Name = string.Format("{0}{1}", component.ShortName, localName);
			this.TargetResource = targetResource;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00004C96 File Offset: 0x00002E96
		// (set) Token: 0x06000013 RID: 19 RVA: 0x00004C9E File Offset: 0x00002E9E
		public Component Component { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00004CA7 File Offset: 0x00002EA7
		public string ServiceName
		{
			get
			{
				return this.Component.Name;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00004CB4 File Offset: 0x00002EB4
		// (set) Token: 0x06000016 RID: 22 RVA: 0x00004CBC File Offset: 0x00002EBC
		public string Name { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00004CC5 File Offset: 0x00002EC5
		// (set) Token: 0x06000018 RID: 24 RVA: 0x00004CCD File Offset: 0x00002ECD
		public string TargetResource { get; private set; }

		// Token: 0x06000019 RID: 25 RVA: 0x00004CD6 File Offset: 0x00002ED6
		public string GetAlertMask()
		{
			if (!string.IsNullOrWhiteSpace(this.TargetResource))
			{
				return string.Format("{0}/{1}", this.Name, this.TargetResource);
			}
			return this.Name;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00004D02 File Offset: 0x00002F02
		public string GetIdentity(bool useTargetResource = true)
		{
			return string.Format((string.IsNullOrWhiteSpace(this.TargetResource) || !useTargetResource) ? "{0}\\{1}" : "{0}\\{1}\\{2}", this.Component.Name, this.Name, this.TargetResource);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00004D3C File Offset: 0x00002F3C
		public override bool Equals(object obj)
		{
			WorkItemIdentity workItemIdentity = obj as WorkItemIdentity;
			return workItemIdentity != null && workItemIdentity.GetIdentity(true) == this.GetIdentity(true);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00004D68 File Offset: 0x00002F68
		public override int GetHashCode()
		{
			return this.GetIdentity(true).GetHashCode();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00004D76 File Offset: 0x00002F76
		public override string ToString()
		{
			return this.GetIdentity(true);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00004D7F File Offset: 0x00002F7F
		protected static string ToLocalName(string baseName, string standardSuffix)
		{
			return baseName + standardSuffix;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00004D88 File Offset: 0x00002F88
		protected static string GetLocalName(Component component, string name, string standardSuffix)
		{
			if (!name.EndsWith(standardSuffix))
			{
				throw new ArgumentException(string.Format("WorkItem name {0} doesn't end with the expected suffix {1}", name, standardSuffix), "baseName");
			}
			string shortName = component.ShortName;
			if (!name.StartsWith(shortName))
			{
				throw new ArgumentException(string.Format("WorkItem name {0} doesn't start with the expected prefix {1}", name, shortName), "baseName");
			}
			return name.Substring(shortName.Length, name.Length - shortName.Length - standardSuffix.Length);
		}

		// Token: 0x02000008 RID: 8
		public class Typed<TDefinition> : WorkItemIdentity where TDefinition : WorkDefinition
		{
			// Token: 0x06000020 RID: 32 RVA: 0x00004DFC File Offset: 0x00002FFC
			protected Typed(Component component, string localName, string targetResource) : base(component, localName, targetResource)
			{
			}

			// Token: 0x06000021 RID: 33 RVA: 0x00004E07 File Offset: 0x00003007
			public virtual void ApplyTo(TDefinition definition)
			{
				definition.ServiceName = base.ServiceName;
				definition.Name = base.Name;
				definition.TargetResource = base.TargetResource;
			}
		}
	}
}
