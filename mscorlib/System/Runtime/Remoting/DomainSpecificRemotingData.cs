using System;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Lifetime;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting
{
	// Token: 0x02000781 RID: 1921
	internal class DomainSpecificRemotingData
	{
		// Token: 0x060053EF RID: 21487 RVA: 0x00129814 File Offset: 0x00127A14
		internal DomainSpecificRemotingData()
		{
			this._flags = 0;
			this._ConfigLock = new object();
			this._ChannelServicesData = new ChannelServicesData();
			this._IDTableLock = new ReaderWriterLock();
			this._appDomainProperties = new IContextProperty[1];
			this._appDomainProperties[0] = new LeaseLifeTimeServiceProperty();
		}

		// Token: 0x17000DDF RID: 3551
		// (get) Token: 0x060053F0 RID: 21488 RVA: 0x00129868 File Offset: 0x00127A68
		// (set) Token: 0x060053F1 RID: 21489 RVA: 0x00129870 File Offset: 0x00127A70
		internal LeaseManager LeaseManager
		{
			get
			{
				return this._LeaseManager;
			}
			set
			{
				this._LeaseManager = value;
			}
		}

		// Token: 0x17000DE0 RID: 3552
		// (get) Token: 0x060053F2 RID: 21490 RVA: 0x00129879 File Offset: 0x00127A79
		internal object ConfigLock
		{
			get
			{
				return this._ConfigLock;
			}
		}

		// Token: 0x17000DE1 RID: 3553
		// (get) Token: 0x060053F3 RID: 21491 RVA: 0x00129881 File Offset: 0x00127A81
		internal ReaderWriterLock IDTableLock
		{
			get
			{
				return this._IDTableLock;
			}
		}

		// Token: 0x17000DE2 RID: 3554
		// (get) Token: 0x060053F4 RID: 21492 RVA: 0x00129889 File Offset: 0x00127A89
		// (set) Token: 0x060053F5 RID: 21493 RVA: 0x00129891 File Offset: 0x00127A91
		internal LocalActivator LocalActivator
		{
			[SecurityCritical]
			get
			{
				return this._LocalActivator;
			}
			[SecurityCritical]
			set
			{
				this._LocalActivator = value;
			}
		}

		// Token: 0x17000DE3 RID: 3555
		// (get) Token: 0x060053F6 RID: 21494 RVA: 0x0012989A File Offset: 0x00127A9A
		// (set) Token: 0x060053F7 RID: 21495 RVA: 0x001298A2 File Offset: 0x00127AA2
		internal ActivationListener ActivationListener
		{
			get
			{
				return this._ActivationListener;
			}
			set
			{
				this._ActivationListener = value;
			}
		}

		// Token: 0x17000DE4 RID: 3556
		// (get) Token: 0x060053F8 RID: 21496 RVA: 0x001298AB File Offset: 0x00127AAB
		// (set) Token: 0x060053F9 RID: 21497 RVA: 0x001298B8 File Offset: 0x00127AB8
		internal bool InitializingActivation
		{
			get
			{
				return (this._flags & 1) == 1;
			}
			set
			{
				if (value)
				{
					this._flags |= 1;
					return;
				}
				this._flags &= -2;
			}
		}

		// Token: 0x17000DE5 RID: 3557
		// (get) Token: 0x060053FA RID: 21498 RVA: 0x001298DB File Offset: 0x00127ADB
		// (set) Token: 0x060053FB RID: 21499 RVA: 0x001298E8 File Offset: 0x00127AE8
		internal bool ActivationInitialized
		{
			get
			{
				return (this._flags & 2) == 2;
			}
			set
			{
				if (value)
				{
					this._flags |= 2;
					return;
				}
				this._flags &= -3;
			}
		}

		// Token: 0x17000DE6 RID: 3558
		// (get) Token: 0x060053FC RID: 21500 RVA: 0x0012990B File Offset: 0x00127B0B
		// (set) Token: 0x060053FD RID: 21501 RVA: 0x00129918 File Offset: 0x00127B18
		internal bool ActivatorListening
		{
			get
			{
				return (this._flags & 4) == 4;
			}
			set
			{
				if (value)
				{
					this._flags |= 4;
					return;
				}
				this._flags &= -5;
			}
		}

		// Token: 0x17000DE7 RID: 3559
		// (get) Token: 0x060053FE RID: 21502 RVA: 0x0012993B File Offset: 0x00127B3B
		internal IContextProperty[] AppDomainContextProperties
		{
			get
			{
				return this._appDomainProperties;
			}
		}

		// Token: 0x17000DE8 RID: 3560
		// (get) Token: 0x060053FF RID: 21503 RVA: 0x00129943 File Offset: 0x00127B43
		internal ChannelServicesData ChannelServicesData
		{
			get
			{
				return this._ChannelServicesData;
			}
		}

		// Token: 0x04002670 RID: 9840
		private const int ACTIVATION_INITIALIZING = 1;

		// Token: 0x04002671 RID: 9841
		private const int ACTIVATION_INITIALIZED = 2;

		// Token: 0x04002672 RID: 9842
		private const int ACTIVATOR_LISTENING = 4;

		// Token: 0x04002673 RID: 9843
		[SecurityCritical]
		private LocalActivator _LocalActivator;

		// Token: 0x04002674 RID: 9844
		private ActivationListener _ActivationListener;

		// Token: 0x04002675 RID: 9845
		private IContextProperty[] _appDomainProperties;

		// Token: 0x04002676 RID: 9846
		private int _flags;

		// Token: 0x04002677 RID: 9847
		private object _ConfigLock;

		// Token: 0x04002678 RID: 9848
		private ChannelServicesData _ChannelServicesData;

		// Token: 0x04002679 RID: 9849
		private LeaseManager _LeaseManager;

		// Token: 0x0400267A RID: 9850
		private ReaderWriterLock _IDTableLock;
	}
}
