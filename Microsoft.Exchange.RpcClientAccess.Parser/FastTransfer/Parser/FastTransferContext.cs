using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser
{
	// Token: 0x02000158 RID: 344
	[DebuggerNonUserCode]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class FastTransferContext<TDataInterface> : BaseObject where TDataInterface : class, IFastTransferDataInterface
	{
		// Token: 0x0600064F RID: 1615 RVA: 0x00012344 File Offset: 0x00010544
		protected FastTransferContext(IResourceTracker resourceTracker, IPropertyFilterFactory propertyFilterFactory, bool isMovingMailbox)
		{
			this.resourceTracker = resourceTracker;
			this.propertyFilterFactory = propertyFilterFactory;
			this.isMovingMailbox = isMovingMailbox;
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000650 RID: 1616 RVA: 0x00012377 File Offset: 0x00010577
		public bool IsMovingMailbox
		{
			get
			{
				return this.isMovingMailbox;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000651 RID: 1617 RVA: 0x00012380 File Offset: 0x00010580
		// (set) Token: 0x06000652 RID: 1618 RVA: 0x000123A6 File Offset: 0x000105A6
		public FastTransferState State
		{
			get
			{
				FastTransferState? fastTransferState = this.state;
				if (fastTransferState == null)
				{
					return FastTransferState.Partial;
				}
				return fastTransferState.GetValueOrDefault();
			}
			protected set
			{
				this.state = new FastTransferState?(value);
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x000123B4 File Offset: 0x000105B4
		public IPropertyFilterFactory PropertyFilterFactory
		{
			get
			{
				return this.propertyFilterFactory;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x000123BC File Offset: 0x000105BC
		public IResourceTracker ResourceTracker
		{
			get
			{
				return this.resourceTracker;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x000123C4 File Offset: 0x000105C4
		internal FastTransferStateMachineFactory StateMachineFactory
		{
			get
			{
				base.CheckDisposed();
				return this.stateMachineFactoryInstance;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x000123D2 File Offset: 0x000105D2
		internal TDataInterface DataInterface
		{
			get
			{
				return this.currentDataInterface;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000657 RID: 1623 RVA: 0x000123DC File Offset: 0x000105DC
		private FastTransferStateMachine? Top
		{
			get
			{
				if (this.fastTransferStack.Count <= 0)
				{
					return null;
				}
				return new FastTransferStateMachine?(this.fastTransferStack.Peek());
			}
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00012414 File Offset: 0x00010614
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("({0})", this.State);
			foreach (FastTransferStateMachine fastTransferStateMachine in this.fastTransferStack)
			{
				stringBuilder.Append(" <= ");
				stringBuilder.Append(fastTransferStateMachine);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x0001249C File Offset: 0x0001069C
		protected static FastTransferStateMachine CreateStateMachine<TContext>(TContext context, IFastTransferProcessor<TContext> fastTransferObject) where TContext : FastTransferContext<TDataInterface>
		{
			FastTransferStateMachine result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				disposeGuard.Add<IFastTransferProcessor<TContext>>(fastTransferObject);
				IEnumerator<FastTransferStateMachine?> enumerator = fastTransferObject.Process(context);
				disposeGuard.Add<IEnumerator<FastTransferStateMachine?>>(enumerator);
				FastTransferStateMachine fastTransferStateMachine = new FastTransferStateMachine(fastTransferObject, enumerator);
				disposeGuard.Success();
				result = fastTransferStateMachine;
			}
			return result;
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00012500 File Offset: 0x00010700
		protected void PushInitial(FastTransferStateMachine stateMachine)
		{
			if (this.state != null || this.fastTransferStack.Count > 0)
			{
				throw new InvalidOperationException("The fast transfer context has alread been primed");
			}
			this.Push(stateMachine);
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00012530 File Offset: 0x00010730
		protected virtual void Process(TDataInterface dataInterface)
		{
			if (this.state == FastTransferState.Error)
			{
				if (this.unexpectedExceptionDispatchInfo != null)
				{
					this.unexpectedExceptionDispatchInfo.Throw();
				}
				return;
			}
			if (this.state == null && this.Top == null)
			{
				throw new InvalidOperationException("Context has not yet been primed with a FastTransferObject");
			}
			TDataInterface tdataInterface = Interlocked.Exchange<TDataInterface>(ref this.currentDataInterface, dataInterface);
			FastTransferStateMachine? fastTransferStateMachine = null;
			try
			{
				this.state = new FastTransferState?(FastTransferState.Error);
				FastTransferStateMachine? fastTransferStateMachine2 = this.Top;
				while (this.CanContinue() && fastTransferStateMachine2 != null)
				{
					try
					{
						fastTransferStateMachine = fastTransferStateMachine2.Value.Step();
					}
					catch (RopExecutionException ex)
					{
						throw new RopExecutionException(string.Format("State machine stalled. Stack: {0}", this), ex.ErrorCode, ex);
					}
					if (fastTransferStateMachine != null)
					{
						if (!fastTransferStateMachine.Value.Equals(fastTransferStateMachine2.Value))
						{
							this.Push(fastTransferStateMachine.Value);
						}
						fastTransferStateMachine2 = fastTransferStateMachine;
						fastTransferStateMachine = null;
					}
					else
					{
						this.Pop(fastTransferStateMachine2.Value);
						fastTransferStateMachine2 = this.Top;
					}
					TDataInterface dataInterface2 = this.DataInterface;
					dataInterface2.NotifyCanSplitBuffers();
				}
				this.state = new FastTransferState?((fastTransferStateMachine2 == null) ? FastTransferState.Done : FastTransferState.Partial);
			}
			catch (Exception source)
			{
				this.unexpectedExceptionDispatchInfo = ExceptionDispatchInfo.Capture(source);
				throw;
			}
			finally
			{
				if (fastTransferStateMachine != null)
				{
					fastTransferStateMachine.Value.Dispose();
				}
				TDataInterface tdataInterface2 = Interlocked.Exchange<TDataInterface>(ref this.currentDataInterface, default(TDataInterface));
			}
		}

		// Token: 0x0600065C RID: 1628
		protected abstract bool CanContinue();

		// Token: 0x0600065D RID: 1629 RVA: 0x000126F8 File Offset: 0x000108F8
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<FastTransferContext<TDataInterface>>(this);
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x00012700 File Offset: 0x00010900
		protected override void InternalDispose()
		{
			foreach (FastTransferStateMachine fastTransferStateMachine in this.fastTransferStack)
			{
				fastTransferStateMachine.Dispose();
			}
			base.InternalDispose();
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x0001275C File Offset: 0x0001095C
		private void Push(FastTransferStateMachine stateMachine)
		{
			this.fastTransferStack.Push(stateMachine);
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x0001276C File Offset: 0x0001096C
		private void Pop(FastTransferStateMachine doneStateMachine)
		{
			using (this.fastTransferStack.Pop())
			{
			}
		}

		// Token: 0x0400033B RID: 827
		private const FastTransferState DefaultState = FastTransferState.Partial;

		// Token: 0x0400033C RID: 828
		private readonly Stack<FastTransferStateMachine> fastTransferStack = new Stack<FastTransferStateMachine>();

		// Token: 0x0400033D RID: 829
		private readonly FastTransferStateMachineFactory stateMachineFactoryInstance = new FastTransferStateMachineFactory();

		// Token: 0x0400033E RID: 830
		private readonly IPropertyFilterFactory propertyFilterFactory;

		// Token: 0x0400033F RID: 831
		private readonly IResourceTracker resourceTracker;

		// Token: 0x04000340 RID: 832
		private readonly bool isMovingMailbox;

		// Token: 0x04000341 RID: 833
		private TDataInterface currentDataInterface;

		// Token: 0x04000342 RID: 834
		private FastTransferState? state;

		// Token: 0x04000343 RID: 835
		private ExceptionDispatchInfo unexpectedExceptionDispatchInfo;
	}
}
