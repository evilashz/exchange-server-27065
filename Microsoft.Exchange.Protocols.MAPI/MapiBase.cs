using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.Mapi;
using Microsoft.Exchange.RpcClientAccess.Parser;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x0200000F RID: 15
	public abstract class MapiBase : DisposableBase, ICriticalBlockFailureHandler, IMapiObject, IDisposable, IServerObject, ICountableObject
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00003136 File Offset: 0x00001336
		public MapiBase(MapiObjectType mapiObjectType)
		{
			this.mapiObjectType = mapiObjectType;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00003145 File Offset: 0x00001345
		internal MapiObjectType MapiObjectType
		{
			get
			{
				return this.mapiObjectType;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000042 RID: 66 RVA: 0x0000314D File Offset: 0x0000134D
		internal ObjectType PropTagObjectType
		{
			get
			{
				return Helper.GetPropTagObjectType(this.MapiObjectType);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000043 RID: 67 RVA: 0x0000315A File Offset: 0x0000135A
		internal ObjectType PropTagBaseObjectType
		{
			get
			{
				return WellKnownProperties.BaseObjectType[(int)this.PropTagObjectType];
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00003168 File Offset: 0x00001368
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00003170 File Offset: 0x00001370
		public bool IsValid
		{
			get
			{
				return this.valid;
			}
			protected set
			{
				this.valid = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00003179 File Offset: 0x00001379
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00003181 File Offset: 0x00001381
		public MapiLogon Logon
		{
			get
			{
				return this.logon;
			}
			protected set
			{
				this.logon = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000048 RID: 72 RVA: 0x0000318A File Offset: 0x0000138A
		public virtual MapiSession Session
		{
			get
			{
				return this.logon.Session;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00003197 File Offset: 0x00001397
		public MapiContext CurrentOperationContext
		{
			get
			{
				return (MapiContext)this.Logon.StoreMailbox.CurrentOperationContext;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600004A RID: 74 RVA: 0x000031AE File Offset: 0x000013AE
		// (set) Token: 0x0600004B RID: 75 RVA: 0x000031B6 File Offset: 0x000013B6
		public MapiPropBagBase ParentObject
		{
			get
			{
				return this.parent;
			}
			protected set
			{
				this.parent = value;
				this.parent.AddSubObject(this);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600004C RID: 76 RVA: 0x000031CB File Offset: 0x000013CB
		public Encoding String8Encoding
		{
			get
			{
				return Encoding.Default;
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000031D2 File Offset: 0x000013D2
		public static IDisposable SetOnDisposeTestHook(Action<MapiBase> action)
		{
			return MapiBase.onDisposeHook.SetTestHook(action);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000031E0 File Offset: 0x000013E0
		public void ThrowIfNotValid(string errorMessage)
		{
			if (!this.valid)
			{
				ExTraceGlobals.GeneralTracer.TraceError(0L, "This " + this.MapiObjectType.ToString() + " object is not valid!  Throwing ExExceptionInvalidObject!");
				throw new ExExceptionInvalidObject((LID)42296U, (errorMessage == null) ? ("This " + this.MapiObjectType.ToString() + " object is not valid.") : errorMessage);
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003255 File Offset: 0x00001455
		public virtual void IncrementObjectCounter(MapiObjectTrackingScope scope, MapiObjectTrackedType trackedType)
		{
			if ((scope & MapiObjectTrackingScope.Session) != (MapiObjectTrackingScope)0U)
			{
				this.objectCounter = this.Logon.Session.GetPerSessionObjectCounter(trackedType);
				this.objectCounter.IncrementCount();
				this.objectCounter.CheckObjectQuota(false);
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000328A File Offset: 0x0000148A
		public virtual void DecrementObjectCounter(MapiObjectTrackingScope scope)
		{
			if ((scope & MapiObjectTrackingScope.Session) != (MapiObjectTrackingScope)0U && this.objectCounter != null)
			{
				this.objectCounter.DecrementCount();
				this.objectCounter = null;
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000032AB File Offset: 0x000014AB
		public virtual IMapiObjectCounter GetObjectCounter(MapiObjectTrackingScope scope)
		{
			if ((scope & MapiObjectTrackingScope.Session) != (MapiObjectTrackingScope)0U && this.objectCounter != null)
			{
				return this.objectCounter;
			}
			return UnlimitedObjectCounter.Instance;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000032C6 File Offset: 0x000014C6
		void ICriticalBlockFailureHandler.OnCriticalBlockFailed(LID lid, Context context, CriticalBlockScope criticalBlockScope)
		{
			this.valid = false;
			context.OnCriticalBlockFailed(lid, criticalBlockScope);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000032D7 File Offset: 0x000014D7
		public virtual void OnRelease(MapiContext context)
		{
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000032D9 File Offset: 0x000014D9
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MapiBase>(this);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000032E4 File Offset: 0x000014E4
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				if (MapiBase.onDisposeHook.Value != null)
				{
					MapiBase.onDisposeHook.Value(this);
				}
				if (this.parent != null)
				{
					this.parent.RemoveSubObject(this);
				}
				this.DecrementObjectCounter(MapiObjectTrackingScope.All);
			}
			this.logon = null;
			this.parent = null;
			this.valid = false;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003340 File Offset: 0x00001540
		public virtual void FormatDiagnosticInformation(TraceContentBuilder cb, int indentLevel)
		{
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003342 File Offset: 0x00001542
		public virtual void ClearDiagnosticInformation()
		{
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003344 File Offset: 0x00001544
		public virtual void GetSummaryInformation(ref ExecutionDiagnostics.LongOperationSummary summary)
		{
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003348 File Offset: 0x00001548
		protected void TraceNotificationIgnored(NotificationEvent nev, string reasonIgnored)
		{
			if (ExTraceGlobals.NotificationTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				StringBuilder stringBuilder = new StringBuilder(100);
				stringBuilder.Append(base.GetType().Name);
				stringBuilder.Append(" has ignored a notification: ");
				stringBuilder.Append(reasonIgnored);
				stringBuilder.Append(" [");
				nev.AppendToString(stringBuilder);
				stringBuilder.Append("]");
				ExTraceGlobals.NotificationTracer.TraceDebug(0L, stringBuilder.ToString());
			}
		}

		// Token: 0x0400004D RID: 77
		private static Hookable<Action<MapiBase>> onDisposeHook = Hookable<Action<MapiBase>>.Create(false, null);

		// Token: 0x0400004E RID: 78
		private MapiLogon logon;

		// Token: 0x0400004F RID: 79
		private MapiObjectType mapiObjectType;

		// Token: 0x04000050 RID: 80
		private bool valid;

		// Token: 0x04000051 RID: 81
		private MapiPropBagBase parent;

		// Token: 0x04000052 RID: 82
		private IMapiObjectCounter objectCounter;
	}
}
