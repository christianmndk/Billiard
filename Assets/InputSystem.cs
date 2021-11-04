// GENERATED AUTOMATICALLY FROM 'Assets/InputSystem.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputSystem : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputSystem()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputSystem"",
    ""maps"": [
        {
            ""name"": ""Queue"",
            ""id"": ""be81c8ed-d9fc-4ee8-94b3-eb903d9e6775"",
            ""actions"": [
                {
                    ""name"": ""opladeQueue"",
                    ""type"": ""Button"",
                    ""id"": ""47259433-bc85-4277-bd48-8e0df670839e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold(pressPoint=0.5)""
                },
                {
                    ""name"": ""turnQueue"",
                    ""type"": ""Button"",
                    ""id"": ""c8fe6278-6d96-49b0-86ff-25ba0f0721a1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""881d958c-a325-4249-b141-7bdf1fa29bea"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""opladeQueue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""turn"",
                    ""id"": ""b387721b-cfc1-4ac4-8402-daf35f935729"",
                    ""path"": ""1DAxis(minValue=-2,maxValue=2,whichSideWins=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""turnQueue"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""ff560741-a1ff-474a-b765-5275c5fdfc2c"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""turnQueue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""793578ac-89b0-47a7-863a-4d8183b83286"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""turnQueue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Queue
        m_Queue = asset.FindActionMap("Queue", throwIfNotFound: true);
        m_Queue_opladeQueue = m_Queue.FindAction("opladeQueue", throwIfNotFound: true);
        m_Queue_turnQueue = m_Queue.FindAction("turnQueue", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Queue
    private readonly InputActionMap m_Queue;
    private IQueueActions m_QueueActionsCallbackInterface;
    private readonly InputAction m_Queue_opladeQueue;
    private readonly InputAction m_Queue_turnQueue;
    public struct QueueActions
    {
        private @InputSystem m_Wrapper;
        public QueueActions(@InputSystem wrapper) { m_Wrapper = wrapper; }
        public InputAction @opladeQueue => m_Wrapper.m_Queue_opladeQueue;
        public InputAction @turnQueue => m_Wrapper.m_Queue_turnQueue;
        public InputActionMap Get() { return m_Wrapper.m_Queue; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(QueueActions set) { return set.Get(); }
        public void SetCallbacks(IQueueActions instance)
        {
            if (m_Wrapper.m_QueueActionsCallbackInterface != null)
            {
                @opladeQueue.started -= m_Wrapper.m_QueueActionsCallbackInterface.OnOpladeQueue;
                @opladeQueue.performed -= m_Wrapper.m_QueueActionsCallbackInterface.OnOpladeQueue;
                @opladeQueue.canceled -= m_Wrapper.m_QueueActionsCallbackInterface.OnOpladeQueue;
                @turnQueue.started -= m_Wrapper.m_QueueActionsCallbackInterface.OnTurnQueue;
                @turnQueue.performed -= m_Wrapper.m_QueueActionsCallbackInterface.OnTurnQueue;
                @turnQueue.canceled -= m_Wrapper.m_QueueActionsCallbackInterface.OnTurnQueue;
            }
            m_Wrapper.m_QueueActionsCallbackInterface = instance;
            if (instance != null)
            {
                @opladeQueue.started += instance.OnOpladeQueue;
                @opladeQueue.performed += instance.OnOpladeQueue;
                @opladeQueue.canceled += instance.OnOpladeQueue;
                @turnQueue.started += instance.OnTurnQueue;
                @turnQueue.performed += instance.OnTurnQueue;
                @turnQueue.canceled += instance.OnTurnQueue;
            }
        }
    }
    public QueueActions @Queue => new QueueActions(this);
    public interface IQueueActions
    {
        void OnOpladeQueue(InputAction.CallbackContext context);
        void OnTurnQueue(InputAction.CallbackContext context);
    }
}
