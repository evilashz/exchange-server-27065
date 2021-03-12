using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ConfigurationSettings
{
	// Token: 0x020001FD RID: 509
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SettingsContextBase : ISettingsContext, IDiagnosableObject
	{
		// Token: 0x060011AB RID: 4523 RVA: 0x000356BC File Offset: 0x000338BC
		public SettingsContextBase(SettingsContextBase nextContext)
		{
			this.HashableIdentity = Guid.NewGuid().ToString();
			this.NextContext = nextContext;
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x060011AC RID: 4524 RVA: 0x000356EF File Offset: 0x000338EF
		// (set) Token: 0x060011AD RID: 4525 RVA: 0x000356F6 File Offset: 0x000338F6
		public static Func<ISettingsContext> DefaultContextGetter { get; set; }

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x060011AE RID: 4526 RVA: 0x000356FE File Offset: 0x000338FE
		// (set) Token: 0x060011AF RID: 4527 RVA: 0x00035706 File Offset: 0x00033906
		public string HashableIdentity { get; private set; }

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x060011B0 RID: 4528 RVA: 0x0003570F File Offset: 0x0003390F
		// (set) Token: 0x060011B1 RID: 4529 RVA: 0x00035717 File Offset: 0x00033917
		public SettingsContextBase NextContext { get; private set; }

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x060011B2 RID: 4530 RVA: 0x00035720 File Offset: 0x00033920
		public virtual Guid? ServerGuid
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x060011B3 RID: 4531 RVA: 0x00035736 File Offset: 0x00033936
		public virtual string ServerName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x060011B4 RID: 4532 RVA: 0x00035739 File Offset: 0x00033939
		public virtual ServerVersion ServerVersion
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x060011B5 RID: 4533 RVA: 0x0003573C File Offset: 0x0003393C
		public virtual string ServerRole
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x060011B6 RID: 4534 RVA: 0x0003573F File Offset: 0x0003393F
		public virtual string ProcessName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x060011B7 RID: 4535 RVA: 0x00035744 File Offset: 0x00033944
		public virtual Guid? DatabaseGuid
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x060011B8 RID: 4536 RVA: 0x0003575A File Offset: 0x0003395A
		public virtual string DatabaseName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x060011B9 RID: 4537 RVA: 0x0003575D File Offset: 0x0003395D
		public virtual ServerVersion DatabaseVersion
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x060011BA RID: 4538 RVA: 0x00035760 File Offset: 0x00033960
		public virtual Guid? DagOrServerGuid
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x060011BB RID: 4539 RVA: 0x00035776 File Offset: 0x00033976
		public virtual string OrganizationName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x060011BC RID: 4540 RVA: 0x00035779 File Offset: 0x00033979
		public virtual ExchangeObjectVersion OrganizationVersion
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x060011BD RID: 4541 RVA: 0x0003577C File Offset: 0x0003397C
		public virtual Guid? MailboxGuid
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x00035792 File Offset: 0x00033992
		public virtual string GetGenericProperty(string propertyName)
		{
			return null;
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x00035798 File Offset: 0x00033998
		public static List<SettingsContextBase> GetCurrentContexts()
		{
			List<SettingsContextBase> list = new List<SettingsContextBase>();
			for (SettingsContextBase.NestedContext nestedContext = SettingsContextBase.NestedContext.Current; nestedContext != null; nestedContext = nestedContext.ParentContext)
			{
				if (nestedContext.Context != null)
				{
					list.Insert(0, nestedContext.Context);
				}
			}
			return list;
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x000357D4 File Offset: 0x000339D4
		public static void RunOperationInContext(List<SettingsContextBase> contexts, Action operation)
		{
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				if (contexts != null)
				{
					foreach (SettingsContextBase settingsContextBase in contexts)
					{
						disposeGuard.Add<IDisposable>(settingsContextBase.Activate());
					}
				}
				operation();
			}
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x00035858 File Offset: 0x00033A58
		public static IDisposable ActivateContext(ISettingsContextProvider provider)
		{
			if (provider != null)
			{
				SettingsContextBase settingsContextBase = provider.GetSettingsContext() as SettingsContextBase;
				if (settingsContextBase != null)
				{
					return settingsContextBase.Activate();
				}
			}
			return null;
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x0003587F File Offset: 0x00033A7F
		public IDisposable Activate()
		{
			return new SettingsContextBase.NestedContext(this);
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x00035888 File Offset: 0x00033A88
		public virtual XElement GetDiagnosticInfo(string argument)
		{
			XElement xelement = new XElement("context");
			if (this.ServerName != null || this.ServerVersion != null)
			{
				xelement.Add(new XElement("server", new object[]
				{
					new XAttribute("guid", (this.ServerGuid != null) ? this.ServerGuid.Value.ToString() : "null"),
					new XAttribute("name", this.ServerName ?? "null"),
					new XAttribute("role", this.ServerRole ?? "null"),
					new XAttribute("version", (this.ServerVersion != null) ? this.ServerVersion.ToString() : "null")
				}));
			}
			if (this.ProcessName != null)
			{
				xelement.Add(new XElement("process", new XAttribute("name", this.ProcessName)));
			}
			if (this.DagOrServerGuid != null)
			{
				xelement.Add(new XElement("dagorserver", new XAttribute("guid", this.DagOrServerGuid.Value.ToString())));
			}
			if (this.DatabaseName != null || this.DatabaseVersion != null)
			{
				xelement.Add(new XElement("database", new object[]
				{
					new XAttribute("guid", (this.DatabaseGuid != null) ? this.DatabaseGuid.Value.ToString() : "null"),
					new XAttribute("name", this.DatabaseName ?? "null"),
					new XAttribute("version", (this.DatabaseVersion != null) ? this.DatabaseVersion.ToString() : "null")
				}));
			}
			if (this.OrganizationName != null || this.OrganizationVersion != null)
			{
				xelement.Add(new XElement("organization", new object[]
				{
					new XAttribute("name", this.OrganizationName ?? "null"),
					new XAttribute("version", (this.OrganizationVersion != null) ? this.OrganizationVersion.ToString() : "null")
				}));
			}
			if (this.MailboxGuid != null)
			{
				xelement.Add(new XElement("mailbox", new XAttribute("guid", this.MailboxGuid)));
			}
			IDiagnosableObject nextContext = this.NextContext;
			if (nextContext != null)
			{
				XElement diagnosticInfo = nextContext.GetDiagnosticInfo(argument);
				diagnosticInfo.Name = "nextContext";
				xelement.Add(diagnosticInfo);
			}
			return xelement;
		}

		// Token: 0x04000ABB RID: 2747
		public static readonly ISettingsContext EffectiveContext = new SettingsContextBase.EffectiveContextObject();

		// Token: 0x020001FE RID: 510
		private class NestedContext : DisposeTrackableBase
		{
			// Token: 0x060011C5 RID: 4549 RVA: 0x00035BF7 File Offset: 0x00033DF7
			public NestedContext(SettingsContextBase context)
			{
				this.Context = context;
				this.ParentContext = SettingsContextBase.NestedContext.current;
				SettingsContextBase.NestedContext.current = this;
			}

			// Token: 0x17000576 RID: 1398
			// (get) Token: 0x060011C6 RID: 4550 RVA: 0x00035C17 File Offset: 0x00033E17
			public static SettingsContextBase.NestedContext Current
			{
				get
				{
					return SettingsContextBase.NestedContext.current;
				}
			}

			// Token: 0x17000577 RID: 1399
			// (get) Token: 0x060011C7 RID: 4551 RVA: 0x00035C1E File Offset: 0x00033E1E
			// (set) Token: 0x060011C8 RID: 4552 RVA: 0x00035C26 File Offset: 0x00033E26
			public SettingsContextBase.NestedContext ParentContext { get; private set; }

			// Token: 0x17000578 RID: 1400
			// (get) Token: 0x060011C9 RID: 4553 RVA: 0x00035C2F File Offset: 0x00033E2F
			// (set) Token: 0x060011CA RID: 4554 RVA: 0x00035C37 File Offset: 0x00033E37
			public SettingsContextBase Context { get; private set; }

			// Token: 0x060011CB RID: 4555 RVA: 0x00035C40 File Offset: 0x00033E40
			protected override void InternalDispose(bool disposing)
			{
				SettingsContextBase.NestedContext.current = this.ParentContext;
			}

			// Token: 0x060011CC RID: 4556 RVA: 0x00035C4D File Offset: 0x00033E4D
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<SettingsContextBase.NestedContext>(this);
			}

			// Token: 0x04000ABF RID: 2751
			[ThreadStatic]
			private static SettingsContextBase.NestedContext current;
		}

		// Token: 0x020001FF RID: 511
		private class EffectiveContextObject : ISettingsContext
		{
			// Token: 0x17000579 RID: 1401
			// (get) Token: 0x060011CD RID: 4557 RVA: 0x00035C5D File Offset: 0x00033E5D
			Guid? ISettingsContext.ServerGuid
			{
				get
				{
					return SettingsContextBase.EffectiveContextObject.ComputeInheritedValue<Guid?>((SettingsContextBase ctx) => ctx.ServerGuid);
				}
			}

			// Token: 0x1700057A RID: 1402
			// (get) Token: 0x060011CE RID: 4558 RVA: 0x00035C89 File Offset: 0x00033E89
			string ISettingsContext.ServerName
			{
				get
				{
					return SettingsContextBase.EffectiveContextObject.ComputeInheritedValue<string>((SettingsContextBase ctx) => ctx.ServerName);
				}
			}

			// Token: 0x1700057B RID: 1403
			// (get) Token: 0x060011CF RID: 4559 RVA: 0x00035CB5 File Offset: 0x00033EB5
			ServerVersion ISettingsContext.ServerVersion
			{
				get
				{
					return SettingsContextBase.EffectiveContextObject.ComputeInheritedValue<ServerVersion>((SettingsContextBase ctx) => ctx.ServerVersion);
				}
			}

			// Token: 0x1700057C RID: 1404
			// (get) Token: 0x060011D0 RID: 4560 RVA: 0x00035CE1 File Offset: 0x00033EE1
			string ISettingsContext.ServerRole
			{
				get
				{
					return SettingsContextBase.EffectiveContextObject.ComputeInheritedValue<string>((SettingsContextBase ctx) => ctx.ServerRole);
				}
			}

			// Token: 0x1700057D RID: 1405
			// (get) Token: 0x060011D1 RID: 4561 RVA: 0x00035D0D File Offset: 0x00033F0D
			string ISettingsContext.ProcessName
			{
				get
				{
					return SettingsContextBase.EffectiveContextObject.ComputeInheritedValue<string>((SettingsContextBase ctx) => ctx.ProcessName);
				}
			}

			// Token: 0x1700057E RID: 1406
			// (get) Token: 0x060011D2 RID: 4562 RVA: 0x00035D39 File Offset: 0x00033F39
			Guid? ISettingsContext.DatabaseGuid
			{
				get
				{
					return SettingsContextBase.EffectiveContextObject.ComputeInheritedValue<Guid?>((SettingsContextBase ctx) => ctx.DatabaseGuid);
				}
			}

			// Token: 0x1700057F RID: 1407
			// (get) Token: 0x060011D3 RID: 4563 RVA: 0x00035D65 File Offset: 0x00033F65
			string ISettingsContext.DatabaseName
			{
				get
				{
					return SettingsContextBase.EffectiveContextObject.ComputeInheritedValue<string>((SettingsContextBase ctx) => ctx.DatabaseName);
				}
			}

			// Token: 0x17000580 RID: 1408
			// (get) Token: 0x060011D4 RID: 4564 RVA: 0x00035D91 File Offset: 0x00033F91
			ServerVersion ISettingsContext.DatabaseVersion
			{
				get
				{
					return SettingsContextBase.EffectiveContextObject.ComputeInheritedValue<ServerVersion>((SettingsContextBase ctx) => ctx.DatabaseVersion);
				}
			}

			// Token: 0x17000581 RID: 1409
			// (get) Token: 0x060011D5 RID: 4565 RVA: 0x00035DBD File Offset: 0x00033FBD
			Guid? ISettingsContext.DagOrServerGuid
			{
				get
				{
					return SettingsContextBase.EffectiveContextObject.ComputeInheritedValue<Guid?>((SettingsContextBase ctx) => ctx.DagOrServerGuid);
				}
			}

			// Token: 0x17000582 RID: 1410
			// (get) Token: 0x060011D6 RID: 4566 RVA: 0x00035DE9 File Offset: 0x00033FE9
			string ISettingsContext.OrganizationName
			{
				get
				{
					return SettingsContextBase.EffectiveContextObject.ComputeInheritedValue<string>((SettingsContextBase ctx) => ctx.OrganizationName);
				}
			}

			// Token: 0x17000583 RID: 1411
			// (get) Token: 0x060011D7 RID: 4567 RVA: 0x00035E15 File Offset: 0x00034015
			ExchangeObjectVersion ISettingsContext.OrganizationVersion
			{
				get
				{
					return SettingsContextBase.EffectiveContextObject.ComputeInheritedValue<ExchangeObjectVersion>((SettingsContextBase ctx) => ctx.OrganizationVersion);
				}
			}

			// Token: 0x17000584 RID: 1412
			// (get) Token: 0x060011D8 RID: 4568 RVA: 0x00035E41 File Offset: 0x00034041
			Guid? ISettingsContext.MailboxGuid
			{
				get
				{
					return SettingsContextBase.EffectiveContextObject.ComputeInheritedValue<Guid?>((SettingsContextBase ctx) => ctx.MailboxGuid);
				}
			}

			// Token: 0x060011D9 RID: 4569 RVA: 0x00035E7C File Offset: 0x0003407C
			string ISettingsContext.GetGenericProperty(string propertyName)
			{
				return SettingsContextBase.EffectiveContextObject.ComputeInheritedValue<string>((SettingsContextBase ctx) => ctx.GetGenericProperty(propertyName));
			}

			// Token: 0x060011DA RID: 4570 RVA: 0x00035EA8 File Offset: 0x000340A8
			private static T ComputeInheritedValue<T>(Func<SettingsContextBase, T> valueGetter)
			{
				for (SettingsContextBase.NestedContext nestedContext = SettingsContextBase.NestedContext.Current; nestedContext != null; nestedContext = nestedContext.ParentContext)
				{
					T t = SettingsContextBase.EffectiveContextObject.ComputeChainedValue<T>(nestedContext.Context, valueGetter);
					if (t != null && !t.Equals(default(T)))
					{
						return t;
					}
				}
				ISettingsContext settingsContext = (SettingsContextBase.DefaultContextGetter != null) ? SettingsContextBase.DefaultContextGetter() : null;
				return SettingsContextBase.EffectiveContextObject.ComputeChainedValue<T>(settingsContext as SettingsContextBase, valueGetter);
			}

			// Token: 0x060011DB RID: 4571 RVA: 0x00035F1C File Offset: 0x0003411C
			private static T ComputeChainedValue<T>(SettingsContextBase firstContext, Func<SettingsContextBase, T> valueGetter)
			{
				for (SettingsContextBase settingsContextBase = firstContext; settingsContextBase != null; settingsContextBase = settingsContextBase.NextContext)
				{
					T t = valueGetter(settingsContextBase);
					if (t != null && !t.Equals(default(T)))
					{
						return t;
					}
				}
				return default(T);
			}
		}
	}
}
