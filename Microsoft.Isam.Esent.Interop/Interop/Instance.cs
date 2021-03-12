using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using Microsoft.Isam.Esent.Interop.Vista;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000263 RID: 611
	public class Instance : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000A9A RID: 2714 RVA: 0x0001584A File Offset: 0x00013A4A
		[SecurityPermission(SecurityAction.LinkDemand)]
		public Instance(string name) : this(name, name, TermGrbit.None)
		{
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x00015855 File Offset: 0x00013A55
		[SecurityPermission(SecurityAction.LinkDemand)]
		public Instance(string name, string displayName) : this(name, displayName, TermGrbit.None)
		{
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x00015860 File Offset: 0x00013A60
		[SecurityPermission(SecurityAction.LinkDemand)]
		public Instance(string name, string displayName, TermGrbit termGrbit) : base(true)
		{
			this.name = name;
			this.displayName = displayName;
			this.termGrbit = termGrbit;
			RuntimeHelpers.PrepareConstrainedRegions();
			JET_INSTANCE instance;
			try
			{
				base.SetHandle(JET_INSTANCE.Nil.Value);
			}
			finally
			{
				Api.JetCreateInstance2(out instance, this.name, this.displayName, CreateInstanceGrbit.None);
				base.SetHandle(instance.Value);
			}
			this.parameters = new InstanceParameters(instance);
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000A9D RID: 2717 RVA: 0x000158E0 File Offset: 0x00013AE0
		public JET_INSTANCE JetInstance
		{
			[SecurityPermission(SecurityAction.LinkDemand)]
			get
			{
				this.CheckObjectIsNotDisposed();
				return this.CreateInstanceFromHandle();
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000A9E RID: 2718 RVA: 0x000158EE File Offset: 0x00013AEE
		public InstanceParameters Parameters
		{
			[SecurityPermission(SecurityAction.LinkDemand)]
			get
			{
				this.CheckObjectIsNotDisposed();
				return this.parameters;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000A9F RID: 2719 RVA: 0x000158FC File Offset: 0x00013AFC
		// (set) Token: 0x06000AA0 RID: 2720 RVA: 0x0001590A File Offset: 0x00013B0A
		public TermGrbit TermGrbit
		{
			[SecurityPermission(SecurityAction.LinkDemand)]
			get
			{
				this.CheckObjectIsNotDisposed();
				return this.termGrbit;
			}
			[SecurityPermission(SecurityAction.LinkDemand)]
			set
			{
				this.CheckObjectIsNotDisposed();
				this.termGrbit = value;
			}
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x00015919 File Offset: 0x00013B19
		[SecurityPermission(SecurityAction.LinkDemand)]
		public static implicit operator JET_INSTANCE(Instance instance)
		{
			return instance.JetInstance;
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x00015924 File Offset: 0x00013B24
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0} ({1})", new object[]
			{
				this.displayName,
				this.name
			});
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x0001595A File Offset: 0x00013B5A
		[SecurityPermission(SecurityAction.LinkDemand)]
		public void Init()
		{
			this.Init(InitGrbit.None);
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x00015964 File Offset: 0x00013B64
		[SecurityPermission(SecurityAction.LinkDemand)]
		public void Init(InitGrbit grbit)
		{
			this.CheckObjectIsNotDisposed();
			JET_INSTANCE jetInstance = this.JetInstance;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				Api.JetInit2(ref jetInstance, grbit);
			}
			finally
			{
				base.SetHandle(jetInstance.Value);
			}
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x000159AC File Offset: 0x00013BAC
		[SecurityPermission(SecurityAction.LinkDemand)]
		public void Init(JET_RSTINFO recoveryOptions, InitGrbit grbit)
		{
			this.CheckObjectIsNotDisposed();
			JET_INSTANCE jetInstance = this.JetInstance;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				VistaApi.JetInit3(ref jetInstance, recoveryOptions, grbit);
			}
			finally
			{
				base.SetHandle(jetInstance.Value);
			}
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x000159F8 File Offset: 0x00013BF8
		[SecurityPermission(SecurityAction.LinkDemand)]
		public void Term()
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				try
				{
					Api.JetTerm2(this.JetInstance, this.termGrbit);
				}
				catch (EsentDirtyShutdownException)
				{
					base.SetHandleAsInvalid();
					throw;
				}
				base.SetHandleAsInvalid();
			}
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x00015A4C File Offset: 0x00013C4C
		protected override bool ReleaseHandle()
		{
			JET_INSTANCE instance = this.CreateInstanceFromHandle();
			return 0 == Api.Impl.JetTerm2(instance, this.termGrbit);
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x00015A74 File Offset: 0x00013C74
		private JET_INSTANCE CreateInstanceFromHandle()
		{
			return new JET_INSTANCE
			{
				Value = this.handle
			};
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x00015A97 File Offset: 0x00013C97
		[SecurityPermission(SecurityAction.LinkDemand)]
		private void CheckObjectIsNotDisposed()
		{
			if (this.IsInvalid || base.IsClosed)
			{
				throw new ObjectDisposedException("Instance");
			}
		}

		// Token: 0x04000450 RID: 1104
		private readonly InstanceParameters parameters;

		// Token: 0x04000451 RID: 1105
		private readonly string name;

		// Token: 0x04000452 RID: 1106
		private readonly string displayName;

		// Token: 0x04000453 RID: 1107
		private TermGrbit termGrbit;
	}
}
