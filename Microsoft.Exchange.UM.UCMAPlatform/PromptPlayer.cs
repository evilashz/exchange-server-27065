using System;
using System.Collections;
using System.Globalization;
using Microsoft.Exchange.Audio;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore;
using Microsoft.Rtc.Collaboration.AudioVideo;
using Microsoft.Speech.Synthesis;

namespace Microsoft.Exchange.UM.UcmaPlatform
{
	// Token: 0x02000007 RID: 7
	internal class PromptPlayer : DisposableBase
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002247 File Offset: 0x00000447
		public PromptPlayer(UcmaCallSession session)
		{
			this.diag = new DiagnosticHelper(this, ExTraceGlobals.UCMATracer);
			this.session = session;
			this.CreatePlayer();
			this.CreateSynthesizers();
			this.Subscribe();
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000012 RID: 18 RVA: 0x0000227C File Offset: 0x0000047C
		// (remove) Token: 0x06000013 RID: 19 RVA: 0x000022B4 File Offset: 0x000004B4
		public event EventHandler<PromptPlayer.PlayerCompletedEventArgs> SpeakCompleted;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000014 RID: 20 RVA: 0x000022EC File Offset: 0x000004EC
		// (remove) Token: 0x06000015 RID: 21 RVA: 0x00002324 File Offset: 0x00000524
		public event EventHandler<BookmarkReachedEventArgs> BookmarkReached;

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002359 File Offset: 0x00000559
		public bool MediaDropped
		{
			get
			{
				return this.connectorShouldBeActive && !this.connector.IsActive;
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002373 File Offset: 0x00000573
		public void AttachFlow(AudioVideoFlow flow)
		{
			this.ucmaPlayer.AttachFlow(flow);
			this.connector.AttachFlow(flow);
			this.synth.SetOutputToAudioStream(this.connector, UcmaCallSession.SpeechAudioFormatInfo);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000023A4 File Offset: 0x000005A4
		public void Play(ArrayList prompts, CultureInfo culture, TimeSpan offset)
		{
			this.diag.Trace("PromptPlayer::Play", new object[0]);
			this.activePlayer = this.DeterminePlayerToUse(prompts);
			switch (this.activePlayer)
			{
			case PromptPlayer.PlayerType.Ucma:
				this.StartUcmaPlayer((TempFilePrompt)prompts[0], offset);
				return;
			case PromptPlayer.PlayerType.Synth:
				this.StartSynth(this.BuildPrompts(prompts, culture), offset);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002410 File Offset: 0x00000610
		public void Cancel()
		{
			this.diag.Trace("PromptPlayer::Cancel", new object[0]);
			switch (this.activePlayer)
			{
			case PromptPlayer.PlayerType.Ucma:
				this.CancelUcmaPlayer();
				return;
			case PromptPlayer.PlayerType.Synth:
				this.CancelSynth();
				return;
			default:
				return;
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002458 File Offset: 0x00000658
		public void Skip(TimeSpan timeToSkip)
		{
			try
			{
				switch (this.activePlayer)
				{
				case PromptPlayer.PlayerType.Ucma:
					this.ucmaPlayer.Skip((int)timeToSkip.TotalMilliseconds);
					break;
				case PromptPlayer.PlayerType.Synth:
					this.synth.Skip(timeToSkip);
					break;
				}
			}
			catch (InvalidOperationException ex)
			{
				this.diag.Trace("Ignoring IOP in Skip", new object[]
				{
					ex
				});
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000024D0 File Offset: 0x000006D0
		public void Pause()
		{
			if (this.ucmaPlayer != null && this.ucmaPlayer.State == null)
			{
				this.ucmaPlayer.Pause();
			}
			if (this.synth != null && (this.synth.State == null || this.synth.State == 1))
			{
				this.synth.Pause();
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000252C File Offset: 0x0000072C
		public void Resume()
		{
			if (this.ucmaPlayer != null && this.ucmaPlayer.State == 2)
			{
				this.ucmaPlayer.Start();
			}
			if (this.synth != null && this.synth.State == 2)
			{
				this.synth.Resume();
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000257B File Offset: 0x0000077B
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PromptPlayer>(this);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002584 File Offset: 0x00000784
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.connector != null)
				{
					this.connector.Dispose();
					this.connector = null;
				}
				if (this.synth != null)
				{
					this.synth.Dispose();
					this.synth = null;
				}
				this.DisposeWmaFileSourceObject();
				this.DisposeWmaBackingFile();
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000025D4 File Offset: 0x000007D4
		private void CreatePlayer()
		{
			this.ucmaPlayer = new Player();
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000025E1 File Offset: 0x000007E1
		private void CreateSynthesizers()
		{
			this.connector = new SpeechSynthesisConnector();
			this.connector.AudioFormat = 2;
			this.synth = new SpeechSynthesizer();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002605 File Offset: 0x00000805
		private void Subscribe()
		{
			this.PlayerSubscribe();
			this.SynthSubscribe(this.synth);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002645 File Offset: 0x00000845
		private void PlayerSubscribe()
		{
			this.ucmaPlayer.StateChanged += delegate(object sender, PlayerStateChangedEventArgs args)
			{
				this.session.Serializer.SerializeEvent<PlayerStateChangedEventArgs>(sender, args, new SerializableEventHandler<PlayerStateChangedEventArgs>(this.Player_StateChanged), this.session, false, "Player_StateChanged");
			};
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000026E2 File Offset: 0x000008E2
		private void SynthSubscribe(SpeechSynthesizer s)
		{
			s.BookmarkReached += delegate(object sender, BookmarkReachedEventArgs args)
			{
				this.session.Serializer.SerializeEvent<BookmarkReachedEventArgs>(sender, args, new SerializableEventHandler<BookmarkReachedEventArgs>(this.Synth_BookmarkReached), this.session, false, "Synthesizer_BookmarkReached");
			};
			s.SpeakCompleted += delegate(object sender, SpeakCompletedEventArgs args)
			{
				this.session.Serializer.SerializeEvent<SpeakCompletedEventArgs>(sender, args, new SerializableEventHandler<SpeakCompletedEventArgs>(this.Synth_SpeakCompleted), this.session, false, "Synthesizer_SpeakCompleted");
			};
			s.SpeakStarted += delegate(object sender, SpeakStartedEventArgs args)
			{
				this.session.Serializer.SerializeEvent<SpeakStartedEventArgs>(sender, args, new SerializableEventHandler<SpeakStartedEventArgs>(this.Synth_SpeakStarted), this.session, false, "Synthesizer_SpeakStarted");
			};
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000271C File Offset: 0x0000091C
		private void Player_StateChanged(object sender, PlayerStateChangedEventArgs e)
		{
			if (e.PreviousState == null)
			{
				if (e.State == 1)
				{
					this.FireSpeakCompleted(e.TransitionReason == 0);
					return;
				}
			}
			else if (e.PreviousState == 1 && e.State == null && this.pendingOffset != TimeSpan.Zero)
			{
				this.ucmaPlayer.Skip((int)this.pendingOffset.TotalMilliseconds);
				this.pendingOffset = TimeSpan.Zero;
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002790 File Offset: 0x00000990
		private void Synth_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
		{
			if (e.Cancelled)
			{
				if (e.Error != null)
				{
					this.diag.Trace("PromptPlayer::Synth_SpeakCompleted returned with Cancelled set. Error = {0}", new object[]
					{
						e.Error
					});
				}
				else
				{
					this.diag.Trace("PromptPlayer::Synth_SpeakCompleted returned with Cancelled set. No Error returned", new object[0]);
				}
			}
			this.FireSpeakCompleted(e.Cancelled);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000027F2 File Offset: 0x000009F2
		private void Synth_BookmarkReached(object sender, BookmarkReachedEventArgs e)
		{
			if (this.BookmarkReached != null)
			{
				this.BookmarkReached(sender, e);
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000280C File Offset: 0x00000A0C
		private void Synth_SpeakStarted(object sender, SpeakStartedEventArgs e)
		{
			if (this.synth.State == 2 && this.pendingOffset > TimeSpan.Zero)
			{
				this.synth.Skip(this.pendingOffset);
				this.synth.Resume();
				this.pendingOffset = TimeSpan.Zero;
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002860 File Offset: 0x00000A60
		private void FireSpeakCompleted(bool cancelled)
		{
			this.diag.Trace("PromptPlayer::FireSpeakCompleted", new object[0]);
			this.activePlayer = PromptPlayer.PlayerType.None;
			if (this.SpeakCompleted != null)
			{
				PromptPlayer.PlayerCompletedEventArgs e = new PromptPlayer.PlayerCompletedEventArgs
				{
					Cancelled = cancelled
				};
				this.SpeakCompleted(this, e);
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000028B0 File Offset: 0x00000AB0
		private PromptPlayer.PlayerType DeterminePlayerToUse(ArrayList prompts)
		{
			ValidateArgument.NotNull(prompts, "prompts");
			ExAssert.RetailAssert(prompts.Count > 0, "no prompts!");
			PromptPlayer.PlayerType playerType;
			if (prompts.Count > 1)
			{
				playerType = PromptPlayer.PlayerType.Synth;
			}
			else if (prompts[0] is TempFilePrompt)
			{
				playerType = PromptPlayer.PlayerType.Ucma;
			}
			else
			{
				playerType = PromptPlayer.PlayerType.Synth;
			}
			this.diag.Trace("Chose player type {0}", new object[]
			{
				playerType
			});
			return playerType;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002920 File Offset: 0x00000B20
		private PromptBuilder BuildPrompts(ArrayList prompts, CultureInfo culture)
		{
			PromptBuilder promptBuilder = new PromptBuilder(culture);
			foreach (object obj in prompts)
			{
				Prompt prompt = (Prompt)obj;
				promptBuilder.AppendSsmlMarkup(prompt.ToSsml());
			}
			return promptBuilder;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002984 File Offset: 0x00000B84
		private void StartSynth(PromptBuilder prompts, TimeSpan offset)
		{
			this.connector.Stop();
			this.connectorShouldBeActive = true;
			this.connector.Start();
			if (offset > TimeSpan.Zero)
			{
				this.synth.Pause();
				this.pendingOffset = offset;
			}
			else
			{
				prompts.AppendBreak(PromptPlayer.endMenuBreak);
			}
			this.synth.SpeakAsync(prompts);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000029E7 File Offset: 0x00000BE7
		private void StartUcmaPlayer(TempFilePrompt prompt, TimeSpan offset)
		{
			this.pendingOffset = offset;
			this.ucmaPlayer.Stop();
			this.CreateWmaBackingFile();
			this.WrapPromptInWmaEnvelope(prompt);
			this.PrepareAndSetMediaSource();
			this.SetUcmaProsodyRate(prompt);
			this.ucmaPlayer.Start();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002A20 File Offset: 0x00000C20
		private void SetUcmaProsodyRate(Prompt prompt)
		{
			PlaybackSpeed playbackSpeed = 100;
			string prosodyRate;
			switch (prosodyRate = prompt.ProsodyRate)
			{
			case "-60%":
				playbackSpeed = 50;
				break;
			case "-30%":
				playbackSpeed = 75;
				break;
			case "-15%":
				playbackSpeed = 75;
				break;
			case "+0%":
				playbackSpeed = 100;
				break;
			case "+15%":
				playbackSpeed = 150;
				break;
			case "+30%":
				playbackSpeed = 175;
				break;
			case "+60%":
				playbackSpeed = 200;
				break;
			}
			this.ucmaPlayer.PlaybackSpeed = playbackSpeed;
			this.diag.Trace("Ucma playback speed.  Prosody='{0}', Speed='{1}'", new object[]
			{
				prompt.ProsodyRate,
				playbackSpeed
			});
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002B3C File Offset: 0x00000D3C
		private void WrapPromptInWmaEnvelope(TempFilePrompt prompt)
		{
			this.diag.Trace("PromptPlayer::WrapPromptInWmaEnvelope", new object[0]);
			using (PcmReader pcmReader = new PcmReader(prompt.FileName))
			{
				using (WmaWriter wmaWriter = WmaWriter.Create(this.backingWmaFile.FilePath, pcmReader.WaveFormat, WmaCodec.Pcm))
				{
					MediaMethods.ConvertWavToWma(pcmReader, wmaWriter);
				}
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002BC0 File Offset: 0x00000DC0
		private void CreateWmaBackingFile()
		{
			this.DisposeWmaBackingFile();
			this.backingWmaFile = TempFileFactory.CreateTempWmaFile();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002BD3 File Offset: 0x00000DD3
		private void DisposeWmaBackingFile()
		{
			if (this.backingWmaFile != null)
			{
				this.backingWmaFile.Dispose();
				this.backingWmaFile = null;
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002BF0 File Offset: 0x00000DF0
		private void PrepareAndSetMediaSource()
		{
			this.diag.Trace("PromptPlayer::PrepareAndSetMediaSource", new object[0]);
			this.DisposeWmaFileSourceObject();
			this.wmaFileSourceObject = new WmaFileSource(this.backingWmaFile.FilePath);
			IAsyncResult asyncResult = this.wmaFileSourceObject.BeginPrepareSource(0, null, null);
			this.wmaFileSourceObject.EndPrepareSource(asyncResult);
			this.ucmaPlayer.SetSource(this.wmaFileSourceObject);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002C5B File Offset: 0x00000E5B
		private void DisposeWmaFileSourceObject()
		{
			if (this.wmaFileSourceObject != null)
			{
				this.wmaFileSourceObject.Close();
				this.wmaFileSourceObject = null;
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002C77 File Offset: 0x00000E77
		private void CancelUcmaPlayer()
		{
			this.diag.Trace("PromptPlayer::StopUcmaPlayer", new object[0]);
			if (this.ucmaPlayer.State == null)
			{
				this.ucmaPlayer.Stop();
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002CA7 File Offset: 0x00000EA7
		private void CancelSynth()
		{
			this.synth.SpeakAsyncCancelAll();
			this.connectorShouldBeActive = false;
			this.connector.Stop();
		}

		// Token: 0x04000002 RID: 2
		private static TimeSpan endMenuBreak = TimeSpan.FromMilliseconds(200.0);

		// Token: 0x04000003 RID: 3
		private ITempFile backingWmaFile;

		// Token: 0x04000004 RID: 4
		private WmaFileSource wmaFileSourceObject;

		// Token: 0x04000005 RID: 5
		private Player ucmaPlayer;

		// Token: 0x04000006 RID: 6
		private SpeechSynthesisConnector connector;

		// Token: 0x04000007 RID: 7
		private SpeechSynthesizer synth;

		// Token: 0x04000008 RID: 8
		private PromptPlayer.PlayerType activePlayer;

		// Token: 0x04000009 RID: 9
		private UcmaCallSession session;

		// Token: 0x0400000A RID: 10
		private DiagnosticHelper diag;

		// Token: 0x0400000B RID: 11
		private TimeSpan pendingOffset;

		// Token: 0x0400000C RID: 12
		private bool connectorShouldBeActive;

		// Token: 0x02000008 RID: 8
		private enum PlayerType
		{
			// Token: 0x04000010 RID: 16
			None,
			// Token: 0x04000011 RID: 17
			Ucma,
			// Token: 0x04000012 RID: 18
			Synth
		}

		// Token: 0x02000009 RID: 9
		internal class PlayerCompletedEventArgs : EventArgs
		{
			// Token: 0x17000004 RID: 4
			// (get) Token: 0x0600003A RID: 58 RVA: 0x00002CDB File Offset: 0x00000EDB
			// (set) Token: 0x0600003B RID: 59 RVA: 0x00002CE3 File Offset: 0x00000EE3
			public bool Cancelled { get; set; }
		}
	}
}
