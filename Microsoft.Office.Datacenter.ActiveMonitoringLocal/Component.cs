using System;
using Microsoft.Exchange.Common;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x0200003A RID: 58
	public class Component
	{
		// Token: 0x06000496 RID: 1174 RVA: 0x000119FA File Offset: 0x0000FBFA
		public Component(string name, HealthGroup healthGroup, string escalationTeam = null, string service = null, ManagedAvailabilityPriority priority = ManagedAvailabilityPriority.Low) : this(name, healthGroup, escalationTeam, service, priority, ServerComponentEnum.None)
		{
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00011A0C File Offset: 0x0000FC0C
		public Component(string value)
		{
			if (!string.IsNullOrWhiteSpace(value))
			{
				string[] array = value.Split(new char[]
				{
					'/'
				}, StringSplitOptions.RemoveEmptyEntries);
				if (array.Length >= 2)
				{
					ManagedAvailabilityPriority priority = ManagedAvailabilityPriority.Low;
					HealthGroup healthGroup;
					if (Enum.TryParse<HealthGroup>(array[0], true, out healthGroup))
					{
						if (array.Length == 3 && !Enum.TryParse<ManagedAvailabilityPriority>(array[2], true, out priority))
						{
							priority = ManagedAvailabilityPriority.Low;
						}
						this.Name = array[1];
						this.HealthGroup = healthGroup;
						this.Priority = priority;
						this.ServerComponent = ServerComponentEnum.None;
						Component component = Component.FindWellKnownComponent(this.Name);
						if (component != null)
						{
							this.ServerComponent = component.ServerComponent;
							this.EscalationTeam = component.EscalationTeam;
							this.Service = component.Service;
						}
						return;
					}
				}
			}
			throw new InvalidOperationException(string.Format("Cannot create a Component with value: {0}", value));
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00011AD8 File Offset: 0x0000FCD8
		internal Component(string name, HealthGroup healthGroup, string escalationTeam, string service, ManagedAvailabilityPriority priority, ServerComponentEnum serverComponent)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new InvalidOperationException("Cannot create a Component with a null or empty name.");
			}
			this.Name = name;
			this.HealthGroup = healthGroup;
			this.EscalationTeam = escalationTeam;
			this.Service = service;
			this.Priority = priority;
			this.ServerComponent = serverComponent;
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x00011B2B File Offset: 0x0000FD2B
		// (set) Token: 0x0600049A RID: 1178 RVA: 0x00011B33 File Offset: 0x0000FD33
		internal string Name { get; private set; }

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x00011B3C File Offset: 0x0000FD3C
		// (set) Token: 0x0600049C RID: 1180 RVA: 0x00011B44 File Offset: 0x0000FD44
		internal HealthGroup HealthGroup { get; private set; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x00011B4D File Offset: 0x0000FD4D
		// (set) Token: 0x0600049E RID: 1182 RVA: 0x00011B55 File Offset: 0x0000FD55
		internal string EscalationTeam { get; private set; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x00011B5E File Offset: 0x0000FD5E
		// (set) Token: 0x060004A0 RID: 1184 RVA: 0x00011B66 File Offset: 0x0000FD66
		internal string Service { get; private set; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x00011B6F File Offset: 0x0000FD6F
		// (set) Token: 0x060004A2 RID: 1186 RVA: 0x00011B77 File Offset: 0x0000FD77
		internal ManagedAvailabilityPriority Priority { get; private set; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x00011B80 File Offset: 0x0000FD80
		// (set) Token: 0x060004A4 RID: 1188 RVA: 0x00011B88 File Offset: 0x0000FD88
		internal ServerComponentEnum ServerComponent { get; private set; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x00011B94 File Offset: 0x0000FD94
		public string ShortName
		{
			get
			{
				return this.Name.Split(new char[]
				{
					'.'
				})[0];
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x00011BBC File Offset: 0x0000FDBC
		public string SubsetName
		{
			get
			{
				int num = this.Name.IndexOf('.');
				if (num == -1)
				{
					return string.Empty;
				}
				return this.Name.Substring(num + 1, this.Name.Length - num - 1);
			}
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00011C00 File Offset: 0x0000FE00
		public static Component FindWellKnownComponent(string componentName)
		{
			Component result = null;
			string[] array = componentName.Split(new char[]
			{
				'/'
			}, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length > 1)
			{
				componentName = array[1];
			}
			ExchangeComponent.WellKnownComponents.TryGetValue(componentName, out result);
			return result;
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00011C40 File Offset: 0x0000FE40
		public static bool operator ==(Component a, Component b)
		{
			return (a == null && b == null) || (a != null && b != null && a.ToString().Equals(b.ToString()));
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x00011C72 File Offset: 0x0000FE72
		public static bool operator !=(Component a, Component b)
		{
			return !(a == b);
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00011C80 File Offset: 0x0000FE80
		public override bool Equals(object obj)
		{
			Component component = obj as Component;
			return component != null && this == component;
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00011CA0 File Offset: 0x0000FEA0
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00011CAD File Offset: 0x0000FEAD
		public override string ToString()
		{
			if (!string.IsNullOrWhiteSpace(this.Name))
			{
				return string.Format("{0}/{1}/{2}", this.HealthGroup.ToString(), this.Name, this.Priority);
			}
			return string.Empty;
		}

		// Token: 0x0400033E RID: 830
		private const char SeparatorChar = '/';
	}
}
