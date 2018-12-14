/************************************************************************************
 Copyright: Copyright 2014 Beijing Noitom Technology Ltd. All Rights reserved.
 Pending Patents: PCT/CN2014/085659 PCT/CN2014/071006

 Licensed under the Perception Neuron SDK License Beta Version (the “License");
 You may only use the Perception Neuron SDK when in compliance with the License,
 which is provided at the time of installation or download, or which
 otherwise accompanies this software in the form of either an electronic or a hard copy.

 A copy of the License is included with this package or can be obtained at:
 http://www.neuronmocap.com

 Unless required by applicable law or agreed to in writing, the Perception Neuron SDK
 distributed under the License is provided on an "AS IS" BASIS,
 WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 See the License for the specific language governing conditions and
 limitations under the License.
************************************************************************************/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using NeuronDataReaderManaged;

namespace Neuron
{
	// NeuronInstance 
	public class NeuronInstance : NetworkBehaviour
	{
        [SyncVar]
        public string						address = "127.0.0.1";
        [SyncVar]
        public int							port = 7001;
        [SyncVar]
        int									commandServerPort = -1;
        [SyncVar]
        public NeuronConnection.SocketType	socketType = NeuronConnection.SocketType.TCP;
        [SyncVar]
        public int							actorID = 0;
        [SyncVar]
        public bool							connectToAxis = false;
        [SyncVar]
        public float 						hipHeightOffset = 0.035f;

        //[SyncVar]
        protected NeuronActor				boundActor = null;
        [SyncVar]
        protected bool						standalone = true;
        [SyncVar]
        protected int						lastEvaluateTime = 0;
        [SyncVar]
        protected bool						boneSizesDirty = false;
        [SyncVar]
        protected bool						applyBoneSizes = false;
		
		public bool							noFrameData { get ; private set; }
		
		public NeuronInstance()
		{
		}
		
		public NeuronInstance( string address, int port, int commandServerPort, NeuronConnection.SocketType socketType, int actorID )
		{
			standalone = true;
		}
		
		public NeuronInstance( NeuronActor boundActor )
		{
			if( boundActor != null )
			{
				this.boundActor = boundActor;
				RegisterCallbacks();
				standalone = false;
			}
		}
		
		public void SetBoundActor( NeuronActor actor )
		{
			if( boundActor != null )
			{
				UnregisterCallbacks();
			}
			
			if( actor != null )
			{
				boundActor = actor;
				RegisterCallbacks();
				actorID = actor.actorID;
				
				NeuronSource source = actor.owner;
				address = source.address;
				port = source.port;
				commandServerPort = source.commandServerPort;
				socketType = source.socketType;
				
				standalone = false;
			}
		}
		
		protected void RegisterCallbacks()
		{
			if( boundActor != null )
			{
				boundActor.RegisterNoFrameDataCallback( OnNoFrameData );
				boundActor.RegisterResumeFrameDataCallback( OnResumeFrameData );
			}
		}
		
		protected void UnregisterCallbacks()
		{
			if( boundActor != null )
			{
				boundActor.UnregisterNoFrameDataCallback( OnNoFrameData );
				boundActor.UnregisterResumeFrameDataCallback( OnResumeFrameData );
			}
		}
		
		protected void OnEnable()
		{
			ToggleConnect();
		}
		
		protected void OnDisable()
		{
			if( boundActor != null && standalone )
			{
				Disconnect();
				boundActor = null;
			}
		}
		
		protected void Update()
		{
			if( standalone && boundActor != null )
			{
				boundActor.owner.OnUpdate();
			}
		}
		
		protected void ToggleConnect()
		{
			if( standalone && connectToAxis && boundActor == null )
			{
				connectToAxis = Connect();
			}
			else if( standalone && !connectToAxis && boundActor != null )
			{
				Disconnect();
			}
		}
		
		protected bool Connect()
		{		
			NeuronSource source = NeuronConnection.Connect( address, port, commandServerPort, socketType );
			if( source != null )
			{
				boundActor = source.AcquireActor( actorID );
				RegisterCallbacks();
			}
			
			return source != null;
		}
		
		protected void Disconnect()
		{
			NeuronConnection.Disconnect( boundActor.owner );
			UnregisterCallbacks();
			boundActor = null;
		}
		
		public virtual bool OnNoFrameData()
		{
			noFrameData = true;
			return false;
		}
		
		public virtual bool OnResumeFrameData()
		{
			noFrameData = false;
			return false;
		}
		
		public virtual bool OnReceivedBoneSizes()
		{
			boneSizesDirty = applyBoneSizes;
			return false;
		}
		
		public NeuronActor GetActor()
		{
			return boundActor;
		}
				
		protected static float CalculateSwapRatio( int timeStamp, ref int last_evaluate_time )
		{
			int now = NeuronActor.GetTimeStamp();
			float swap_ratio = (float)( timeStamp - last_evaluate_time ) / (float)( now - last_evaluate_time );
			last_evaluate_time = now;
			return Mathf.Clamp( swap_ratio, 0.0f, 1.0f );
		}
	}
}