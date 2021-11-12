namespace ILRuntime.Moudle.CrossBindingAdaptorCustom
{
    using UnityEngine;
    using System;
    using System.Collections.Generic;
    using ILRuntime.Runtime.Enviorment;
    using ILRuntime.Runtime.Intepreter;
    using ILRuntime.CLR.Method;


    public class MonoBehaviourAdapter : CrossBindingAdaptor
    {
        public override Type BaseCLRType
        {
            get { return typeof(MonoBehaviour); }
        }

        public override Type AdaptorType
        {
            get { return typeof(Adaptor); }
        }

        public override object CreateCLRInstance(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
        {
            return new Adaptor(appdomain, instance);
        }

        //为了完整实现MonoBehaviour的所有特性，这个Adapter还得扩展，这里只抛砖引玉，只实现了最常用的Awake, Start和Update
        public class Adaptor : MonoBehaviour, CrossBindingAdaptorType
        {
            ILTypeInstance instance;
            ILRuntime.Runtime.Enviorment.AppDomain appdomain;

            public Adaptor()
            {
            }

            public Adaptor(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
            {
                this.appdomain = appdomain;
                this.instance = instance;
            }

            public ILTypeInstance ILInstance
            {
                get { return instance; }
                set { instance = value; }
            }

            public ILRuntime.Runtime.Enviorment.AppDomain AppDomain
            {
                get { return appdomain; }
                set { appdomain = value; }
            }

            IMethod mAwakeMethod;
            bool mAwakeMethodGot;

            public void Awake()
            {
                //Unity会在ILRuntime准备好这个实例前调用Awake，所以这里暂时先不掉用
                if (instance != null)
                {
                    if (!mAwakeMethodGot)
                    {
                        mAwakeMethod = instance.Type.GetMethod("Awake", 0);
                        mAwakeMethodGot = true;
                    }

                    if (mAwakeMethod != null)
                    {
                        appdomain.Invoke(mAwakeMethod, instance, null);
                    }
                }
            }

            IMethod mResetMethod;
            bool mResetMethodGot;

            public void Reset()
            {
                if (instance != null)
                {
                    if (!mResetMethodGot)
                    {
                        mResetMethod = instance.Type.GetMethod("Reset", 0);
                        mResetMethodGot = true;
                    }

                    if (mResetMethod != null)
                    {
                        appdomain.Invoke(mResetMethod, instance, null);
                    }
                }
            }

            IMethod mStartMethod;
            bool mStartMethodGot;

            void Start()
            {
                if (!mStartMethodGot)
                {
                    mStartMethod = instance.Type.GetMethod("Start", 0);
                    mStartMethodGot = true;
                }

                if (mStartMethod != null)
                {
                    appdomain.Invoke(mStartMethod, instance, null);
                }
            }

            IMethod mUpdateMethod;
            bool mUpdateMethodGot;

            void Update()
            {
                if (!mUpdateMethodGot)
                {
                    mUpdateMethod = instance.Type.GetMethod("Update", 0);
                    mUpdateMethodGot = true;
                }

                if (mUpdateMethod != null)
                {
                    appdomain.Invoke(mUpdateMethod, instance, null);
                }
            }

            IMethod mFixedUpdateMethod;
            bool mFixedUpdateMethodGot;

            void FixedUpdate()
            {
                if (!mFixedUpdateMethodGot)
                {
                    mFixedUpdateMethod = instance.Type.GetMethod("FixedUpdate", 0);
                    mFixedUpdateMethodGot = true;
                }

                if (mFixedUpdateMethod != null)
                {
                    appdomain.Invoke(mFixedUpdateMethod, instance, null);
                }
            }

            IMethod mLateUpdateMethod;
            bool mLateUpdateMethodGot;

            void LateUpdate()
            {
                if (!mLateUpdateMethodGot)
                {
                    mLateUpdateMethod = instance.Type.GetMethod("LateUpdate", 0);
                    mLateUpdateMethodGot = true;
                }

                if (mLateUpdateMethod != null)
                {
                    appdomain.Invoke(mLateUpdateMethod, instance, null);
                }
            }


            IMethod mOnEnableMethod;
            bool mOnEnableMethodGot;

            public void OnEnable()
            {
                //Unity会在ILRuntime准备好这个实例前调用OnEnable，所以这里暂时先不掉用
                if (instance != null)
                {
                    if (!mOnEnableMethodGot)
                    {
                        List<IMethod> methods = instance.Type.GetMethods();
                        mOnEnableMethod = instance.Type.GetMethod("OnEnable", 0);
                        mOnEnableMethodGot = true;
                    }

                    if (mOnEnableMethod != null)
                    {
                        appdomain.Invoke(mOnEnableMethod, instance, null);
                    }
                }
            }

            IMethod mOnDisableMethod;
            bool mOnDisableMethodGot;

            void OnDisable()
            {
                if (!mOnDisableMethodGot)
                {
                    mOnDisableMethod = instance.Type.GetMethod("OnDisable", 0);
                    mOnDisableMethodGot = true;
                }

                if (mOnDisableMethod != null)
                {
                    appdomain.Invoke(mOnDisableMethod, instance, null);
                }
            }

            IMethod mOnDestroyMethod;
            bool mOnDestroyMethodGot;

            void OnDestroy()
            {
                if (!mOnDestroyMethodGot)
                {
                    mOnDestroyMethod = instance.Type.GetMethod("OnDestroy", 0);
                    mOnDestroyMethodGot = true;
                }

                if (mUpdateMethod != null)
                {
                    appdomain.Invoke(mOnDestroyMethod, instance, null);
                }
            }

            IMethod mOnGUIMethod;
            bool mOnGUIMethodGot;

            void OnGUI()
            {
                if (!mOnGUIMethodGot)
                {
                    mOnGUIMethod = instance.Type.GetMethod("OnGUI", 0);
                    mOnGUIMethodGot = true;
                }

                if (mOnGUIMethod != null)
                {
                    appdomain.Invoke(mOnGUIMethod, instance, null);
                }
            }

            IMethod mOnAnimatorIKMethod;
            bool mOnAnimatorIKMethodGot;

            void OnAnimatorIK(int layerIndex)
            {
                if (!mOnAnimatorIKMethodGot)
                {
                    mOnAnimatorIKMethod = instance.Type.GetMethod("OnAnimatorIK", 1);
                    mOnAnimatorIKMethodGot = true;
                }

                if (mOnAnimatorIKMethod != null)
                {
                    appdomain.Invoke(mOnAnimatorIKMethod, instance, layerIndex);
                }
            }

            IMethod mOnAnimatorMoveMethod;
            bool mOnAnimatorMoveMethodGot;

            void OnAnimatorMove()
            {
                if (!mOnAnimatorMoveMethodGot)
                {
                    mOnAnimatorMoveMethod = instance.Type.GetMethod("OnAnimatorMove", 0);
                    mOnAnimatorMoveMethodGot = true;
                }

                if (mOnAnimatorMoveMethod != null)
                {
                    appdomain.Invoke(mOnAnimatorMoveMethod, instance, null);
                }
            }

            IMethod mOnApplicationFocusMethod;
            bool mOnApplicationFocusMethodGot;

            void OnApplicationFocus(bool hasFocus)
            {
                if (!mOnApplicationFocusMethodGot)
                {
                    mOnApplicationFocusMethod = instance.Type.GetMethod("OnApplicationFocus", 1);
                    mOnApplicationFocusMethodGot = true;
                }

                if (mOnApplicationFocusMethod != null)
                {
                    appdomain.Invoke(mOnApplicationFocusMethod, instance, hasFocus);
                }
            }

            IMethod mOnApplicationPauseMethod;
            bool mOnApplicationPauseMethodGot;

            void OnApplicationPause(bool pauseStatus)
            {
                if (!mOnApplicationPauseMethodGot)
                {
                    mOnApplicationPauseMethod = instance.Type.GetMethod("OnApplicationPause", 1);
                    mOnApplicationPauseMethodGot = true;
                }

                if (mOnApplicationPauseMethod != null)
                {
                    appdomain.Invoke(mOnApplicationPauseMethod, instance, pauseStatus);
                }
            }

            IMethod mOnApplicationQuitMethod;
            bool mOnApplicationQuitMethodGot;

            void OnApplicationQuit()
            {
                if (!mOnApplicationQuitMethodGot)
                {
                    mOnApplicationQuitMethod = instance.Type.GetMethod("OnApplicationQuit", 0);
                    mOnApplicationQuitMethodGot = true;
                }

                if (mOnApplicationQuitMethod != null)
                {
                    appdomain.Invoke(mOnApplicationQuitMethod, instance, null);
                }
            }

            IMethod mOnBecameInvisibleMethod;
            bool mOnBecameInvisibleMethodGot;

            void OnBecameInvisible()
            {
                if (!mOnBecameInvisibleMethodGot)
                {
                    mOnBecameInvisibleMethod = instance.Type.GetMethod("OnBecameInvisible", 0);
                    mOnBecameInvisibleMethodGot = true;
                }

                if (mOnBecameInvisibleMethod != null)
                {
                    appdomain.Invoke(mOnBecameInvisibleMethod, instance, null);
                }
            }

            IMethod mOnBecameVisibleMethod;
            bool mOnBecameVisibleMethodGot;

            void OnBecameVisible()
            {
                if (!mOnBecameVisibleMethodGot)
                {
                    mOnBecameVisibleMethod = instance.Type.GetMethod("OnBecameVisible", 0);
                    mOnBecameVisibleMethodGot = true;
                }

                if (mOnBecameVisibleMethod != null)
                {
                    appdomain.Invoke(mOnBecameVisibleMethod, instance, null);
                }
            }

            IMethod mOnBeforeTransformParentChangedMethod;
            bool mOnBeforeTransformParentChangedMethodGot;

            void OnBeforeTransformParentChanged()
            {
                if (!mOnBeforeTransformParentChangedMethodGot)
                {
                    mOnBeforeTransformParentChangedMethod = instance.Type.GetMethod("OnBeforeTransformParentChanged", 0);
                    mOnBeforeTransformParentChangedMethodGot = true;
                }

                if (mOnBeforeTransformParentChangedMethod != null)
                {
                    appdomain.Invoke(mOnBeforeTransformParentChangedMethod, instance, null);
                }
            }

            IMethod mOnCanvasGroupChangedMethod;
            bool mOnCanvasGroupChangedMethodGot;

            void OnCanvasGroupChanged()
            {
                if (!mOnCanvasGroupChangedMethodGot)
                {
                    mOnCanvasGroupChangedMethod = instance.Type.GetMethod("OnCanvasGroupChanged", 0);
                    mOnCanvasGroupChangedMethodGot = true;
                }

                if (mOnCanvasGroupChangedMethod != null)
                {
                    appdomain.Invoke(mOnCanvasGroupChangedMethod, instance, null);
                }
            }

            IMethod mOnCanvasHierarchyChangedMethod;
            bool mOnCanvasHierarchyChangedMethodGot;

            void OnCanvasHierarchyChanged()
            {
                if (!mOnCanvasHierarchyChangedMethodGot)
                {
                    mOnCanvasHierarchyChangedMethod = instance.Type.GetMethod("OnCanvasHierarchyChanged", 0);
                    mOnCanvasHierarchyChangedMethodGot = true;
                }

                if (mOnCanvasHierarchyChangedMethod != null)
                {
                    appdomain.Invoke(mOnCanvasHierarchyChangedMethod, instance, null);
                }
            }

            IMethod mOnCollisionEnterMethod;
            bool mOnCollisionEnterMethodGot;

            void OnCollisionEnter(Collision other)
            {
                if (!mOnCollisionEnterMethodGot)
                {
                    mOnCollisionEnterMethod = instance.Type.GetMethod("OnCollisionEnter", 1);
                    mOnCollisionEnterMethodGot = true;
                }

                if (mOnCollisionEnterMethod != null)
                {
                    appdomain.Invoke(mOnCollisionEnterMethod, instance, other);
                }
            }

            IMethod mOnCollisionEnter2DMethod;
            bool mOnCollisionEnter2DMethodGot;

            void OnCollisionEnter2D(Collision2D other)
            {
                if (!mOnCollisionEnter2DMethodGot)
                {
                    mOnCollisionEnter2DMethod = instance.Type.GetMethod("OnCollisionEnter2D", 1);
                    mOnCollisionEnter2DMethodGot = true;
                }

                if (mOnCollisionEnter2DMethod != null)
                {
                    appdomain.Invoke(mOnCollisionEnter2DMethod, instance, other);
                }
            }

            IMethod mOnCollisionExitMethod;
            bool mOnCollisionExitMethodGot;

            void OnCollisionExit(Collision other)
            {
                if (!mOnCollisionExitMethodGot)
                {
                    mOnCollisionExitMethod = instance.Type.GetMethod("OnCollisionExit", 1);
                    mOnCollisionExitMethodGot = true;
                }

                if (mOnCollisionExitMethod != null)
                {
                    appdomain.Invoke(mOnCollisionExitMethod, instance, other);
                }
            }

            IMethod mOnCollisionExit2DMethod;
            bool mOnCollisionExit2DMethodGot;

            void OnCollisionExit2D(Collision2D other)
            {
                if (!mOnCollisionExit2DMethodGot)
                {
                    mOnCollisionExit2DMethod = instance.Type.GetMethod("OnCollisionExit2D", 1);
                    mOnCollisionExit2DMethodGot = true;
                }

                if (mOnCollisionExit2DMethod != null)
                {
                    appdomain.Invoke(mOnCollisionExit2DMethod, instance, other);
                }
            }

            IMethod mOnCollisionStayMethod;
            bool mOnCollisionStayMethodGot;

            void OnCollisionStay(Collision other)
            {
                if (!mOnCollisionStayMethodGot)
                {
                    mOnCollisionStayMethod = instance.Type.GetMethod("OnCollisionStay", 1);
                    mOnCollisionStayMethodGot = true;
                }

                if (mOnCollisionStayMethod != null)
                {
                    appdomain.Invoke(mOnCollisionStayMethod, instance, other);
                }
            }

            IMethod mOnCollisionStay2DMethod;
            bool mOnCollisionStay2DMethodGot;

            void OnCollisionStay2D(Collision2D other)
            {
                if (!mOnCollisionStay2DMethodGot)
                {
                    mOnCollisionStay2DMethod = instance.Type.GetMethod("OnCollisionStay2D", 1);
                    mOnCollisionStay2DMethodGot = true;
                }

                if (mOnCollisionStay2DMethod != null)
                {
                    appdomain.Invoke(mOnCollisionStay2DMethod, instance, other);
                }
            }

            IMethod mOnConnectedToServerMethod;
            bool mOnConnectedToServerMethodGot;

            void OnConnectedToServer()
            {
                if (!mOnConnectedToServerMethodGot)
                {
                    mOnConnectedToServerMethod = instance.Type.GetMethod("OnConnectedToServer", 0);
                    mOnConnectedToServerMethodGot = true;
                }

                if (mOnConnectedToServerMethod != null)
                {
                    appdomain.Invoke(mOnConnectedToServerMethod, instance, null);
                }
            }

            IMethod mOnControllerColliderHitMethod;
            bool mOnControllerColliderHitMethodGot;

            void OnControllerColliderHit(ControllerColliderHit hit)
            {
                if (!mOnControllerColliderHitMethodGot)
                {
                    mOnControllerColliderHitMethod = instance.Type.GetMethod("OnControllerColliderHit", 1);
                    mOnControllerColliderHitMethodGot = true;
                }

                if (mOnControllerColliderHitMethod != null)
                {
                    appdomain.Invoke(mOnControllerColliderHitMethod, instance, hit);
                }
            }

            IMethod mOnDidApplyAnimationPropertiesMethod;
            bool mOnDidApplyAnimationPropertiesMethodGot;

            void OnDidApplyAnimationProperties()
            {
                if (!mOnDidApplyAnimationPropertiesMethodGot)
                {
                    mOnDidApplyAnimationPropertiesMethod = instance.Type.GetMethod("OnDidApplyAnimationProperties", 0);
                    mOnDidApplyAnimationPropertiesMethodGot = true;
                }

                if (mOnDidApplyAnimationPropertiesMethod != null)
                {
                    appdomain.Invoke(mOnDidApplyAnimationPropertiesMethod, instance, null);
                }
            }

            IMethod mOnDrawGizmosMethod;
            bool mOnDrawGizmosMethodGot;

            void OnDrawGizmos()
            {
                if (!mOnDrawGizmosMethodGot)
                {
                    mOnDrawGizmosMethod = instance.Type.GetMethod("OnDrawGizmos", 0);
                    mOnDrawGizmosMethodGot = true;
                }

                if (mOnDrawGizmosMethod != null)
                {
                    appdomain.Invoke(mOnDrawGizmosMethod, instance, null);
                }
            }

            IMethod mOnDrawGizmosSelectedMethod;
            bool mOnDrawGizmosSelectedMethodGot;

            void OnDrawGizmosSelected()
            {
                if (!mOnDrawGizmosSelectedMethodGot)
                {
                    mOnDrawGizmosSelectedMethod = instance.Type.GetMethod("OnDrawGizmosSelected", 0);
                    mOnDrawGizmosSelectedMethodGot = true;
                }

                if (mOnDrawGizmosSelectedMethod != null)
                {
                    appdomain.Invoke(mOnDrawGizmosSelectedMethod, instance, null);
                }
            }

            IMethod mOnJointBreakMethod;
            bool mOnJointBreakMethodGot;

            void OnJointBreak(float breakForce)
            {
                if (!mOnJointBreakMethodGot)
                {
                    mOnJointBreakMethod = instance.Type.GetMethod("OnJointBreak", 1);
                    mOnJointBreakMethodGot = true;
                }

                if (mOnJointBreakMethod != null)
                {
                    appdomain.Invoke(mOnJointBreakMethod, instance, breakForce);
                }
            }

            IMethod mOnJointBreak2DMethod;
            bool mOnJointBreak2DMethodGot;

            void OnJointBreak2D(Joint2D brokenJoint)
            {
                if (!mOnJointBreak2DMethodGot)
                {
                    mOnJointBreak2DMethod = instance.Type.GetMethod("OnJointBreak2D", 1);
                    mOnJointBreak2DMethodGot = true;
                }

                if (mOnJointBreak2DMethod != null)
                {
                    appdomain.Invoke(mOnJointBreak2DMethod, instance, brokenJoint);
                }
            }

            IMethod mOnMouseDownMethod;
            bool mOnMouseDownMethodGot;

            void OnMouseDown()
            {
                if (!mOnMouseDownMethodGot)
                {
                    mOnMouseDownMethod = instance.Type.GetMethod("OnMouseDown", 0);
                    mOnMouseDownMethodGot = true;
                }

                if (mOnMouseDownMethod != null)
                {
                    appdomain.Invoke(mOnMouseDownMethod, instance, null);
                }
            }

            IMethod mOnMouseDragMethod;
            bool mOnMouseDragMethodGot;

            void OnMouseDrag()
            {
                if (!mOnMouseDragMethodGot)
                {
                    mOnMouseDragMethod = instance.Type.GetMethod("OnMouseDrag", 0);
                    mOnMouseDragMethodGot = true;
                }

                if (mOnMouseDragMethod != null)
                {
                    appdomain.Invoke(mOnMouseDragMethod, instance, null);
                }
            }

            IMethod mOnMouseEnterMethod;
            bool mOnMouseEnterMethodGot;

            void OnMouseEnter()
            {
                if (!mOnMouseEnterMethodGot)
                {
                    mOnMouseEnterMethod = instance.Type.GetMethod("OnMouseEnter", 0);
                    mOnMouseEnterMethodGot = true;
                }

                if (mOnMouseEnterMethod != null)
                {
                    appdomain.Invoke(mOnMouseEnterMethod, instance, null);
                }
            }

            IMethod mOnMouseExitMethod;
            bool mOnMouseExitMethodGot;

            void OnMouseExit()
            {
                if (!mOnMouseExitMethodGot)
                {
                    mOnMouseExitMethod = instance.Type.GetMethod("OnMouseExit", 0);
                    mOnMouseExitMethodGot = true;
                }

                if (mOnMouseExitMethod != null)
                {
                    appdomain.Invoke(mOnMouseExitMethod, instance, null);
                }
            }

            IMethod mOnMouseOverMethod;
            bool mOnMouseOverMethodGot;

            void OnMouseOver()
            {
                if (!mOnMouseOverMethodGot)
                {
                    mOnMouseOverMethod = instance.Type.GetMethod("OnMouseOver", 0);
                    mOnMouseOverMethodGot = true;
                }

                if (mOnMouseOverMethod != null)
                {
                    appdomain.Invoke(mOnMouseOverMethod, instance, null);
                }
            }

            IMethod mOnMouseUpMethod;
            bool mOnMouseUpMethodGot;

            void OnMouseUp()
            {
                if (!mOnMouseUpMethodGot)
                {
                    mOnMouseUpMethod = instance.Type.GetMethod("OnMouseUp", 0);
                    mOnMouseUpMethodGot = true;
                }

                if (mOnMouseUpMethod != null)
                {
                    appdomain.Invoke(mOnMouseUpMethod, instance, null);
                }
            }

            IMethod mOnMouseUpAsButtonMethod;
            bool mOnMouseUpAsButtonMethodGot;

            void OnMouseUpAsButton()
            {
                if (!mOnMouseUpAsButtonMethodGot)
                {
                    mOnMouseUpAsButtonMethod = instance.Type.GetMethod("OnMouseUpAsButton", 0);
                    mOnMouseUpAsButtonMethodGot = true;
                }

                if (mOnMouseUpAsButtonMethod != null)
                {
                    appdomain.Invoke(mOnMouseUpAsButtonMethod, instance, null);
                }
            }

            IMethod mOnParticleCollisionMethod;
            bool mOnParticleCollisionMethodGot;

            void OnParticleCollision(GameObject other)
            {
                if (!mOnParticleCollisionMethodGot)
                {
                    mOnParticleCollisionMethod = instance.Type.GetMethod("OnParticleCollision", 1);
                    mOnParticleCollisionMethodGot = true;
                }

                if (mOnParticleCollisionMethod != null)
                {
                    appdomain.Invoke(mOnParticleCollisionMethod, instance, other);
                }
            }

            IMethod mOnParticleSystemStoppedMethod;
            bool mOnParticleSystemStoppedMethodGot;

            void OnParticleSystemStopped()
            {
                if (!mOnParticleSystemStoppedMethodGot)
                {
                    mOnParticleSystemStoppedMethod = instance.Type.GetMethod("OnParticleSystemStopped", 0);
                    mOnParticleSystemStoppedMethodGot = true;
                }

                if (mOnParticleSystemStoppedMethod != null)
                {
                    appdomain.Invoke(mOnParticleSystemStoppedMethod, instance, null);
                }
            }

            IMethod mOnParticleTriggerMethod;
            bool mOnParticleTriggerMethodGot;

            void OnParticleTrigger()
            {
                if (!mOnParticleTriggerMethodGot)
                {
                    mOnParticleTriggerMethod = instance.Type.GetMethod("OnParticleTrigger", 0);
                    mOnParticleTriggerMethodGot = true;
                }

                if (mOnParticleTriggerMethod != null)
                {
                    appdomain.Invoke(mOnParticleTriggerMethod, instance, null);
                }
            }

            IMethod mOnParticleUpdateJobScheduledMethod;
            bool mOnParticleUpdateJobScheduledMethodGot;

            void OnParticleUpdateJobScheduled()
            {
                if (!mOnParticleUpdateJobScheduledMethodGot)
                {
                    mOnParticleUpdateJobScheduledMethod = instance.Type.GetMethod("OnParticleUpdateJobScheduled", 0);
                    mOnParticleUpdateJobScheduledMethodGot = true;
                }

                if (mOnParticleUpdateJobScheduledMethod != null)
                {
                    appdomain.Invoke(mOnParticleUpdateJobScheduledMethod, instance, null);
                }
            }

            IMethod mOnPostRenderMethod;
            bool mOnPostRenderMethodGot;

            void OnPostRender()
            {
                if (!mOnPostRenderMethodGot)
                {
                    mOnPostRenderMethod = instance.Type.GetMethod("OnPostRender", 0);
                    mOnPostRenderMethodGot = true;
                }

                if (mOnPostRenderMethod != null)
                {
                    appdomain.Invoke(mOnPostRenderMethod, instance, null);
                }
            }

            IMethod mOnPreCullMethod;
            bool mOnPreCullMethodGot;

            void OnPreCull()
            {
                if (!mOnPreCullMethodGot)
                {
                    mOnPreCullMethod = instance.Type.GetMethod("OnPreCull", 0);
                    mOnPreCullMethodGot = true;
                }

                if (mOnPreCullMethod != null)
                {
                    appdomain.Invoke(mOnPreCullMethod, instance, null);
                }
            }

            IMethod mOnPreRenderMethod;
            bool mOnPreRenderMethodGot;

            void OnPreRender()
            {
                if (!mOnPreRenderMethodGot)
                {
                    mOnPreRenderMethod = instance.Type.GetMethod("OnPreRender", 0);
                    mOnPreRenderMethodGot = true;
                }

                if (mOnPreRenderMethod != null)
                {
                    appdomain.Invoke(mOnPreRenderMethod, instance, null);
                }
            }

            IMethod mOnRectTransformDimensionsChangeMethod;
            bool mOnRectTransformDimensionsChangeMethodGot;

            void OnRectTransformDimensionsChange()
            {
                if (!mOnRectTransformDimensionsChangeMethodGot)
                {
                    mOnRectTransformDimensionsChangeMethod = instance.Type.GetMethod("OnRectTransformDimensionsChange", 0);
                    mOnRectTransformDimensionsChangeMethodGot = true;
                }

                if (mOnRectTransformDimensionsChangeMethod != null)
                {
                    appdomain.Invoke(mOnRectTransformDimensionsChangeMethod, instance, null);
                }
            }

            IMethod mOnRenderImageMethod;
            bool mOnRenderImageMethodGot;

            void OnRenderImage(RenderTexture src, RenderTexture dest)
            {
                if (!mOnRenderImageMethodGot)
                {
                    mOnRenderImageMethod = instance.Type.GetMethod("OnRenderImage", 2);
                    mOnRenderImageMethodGot = true;
                }

                if (mOnRenderImageMethod != null)
                {
                    appdomain.Invoke(mOnRenderImageMethod, instance, src, dest);
                }
            }

            IMethod mOnRenderObjectMethod;
            bool mOnRenderObjectMethodGot;

            void OnRenderObject()
            {
                if (!mOnRenderObjectMethodGot)
                {
                    mOnRenderObjectMethod = instance.Type.GetMethod("OnRenderObject", 0);
                    mOnRenderObjectMethodGot = true;
                }

                if (mOnRenderObjectMethod != null)
                {
                    appdomain.Invoke(mOnRenderObjectMethod, instance, null);
                }
            }

            IMethod mOnServerInitializedMethod;
            bool mOnServerInitializedMethodGot;

            void OnServerInitialized()
            {
                if (!mOnServerInitializedMethodGot)
                {
                    mOnServerInitializedMethod = instance.Type.GetMethod("OnServerInitialized", 0);
                    mOnServerInitializedMethodGot = true;
                }

                if (mOnServerInitializedMethod != null)
                {
                    appdomain.Invoke(mOnServerInitializedMethod, instance, null);
                }
            }

            IMethod mOnTransformChildrenChangedMethod;
            bool mOnTransformChildrenChangedMethodGot;

            void OnTransformChildrenChanged()
            {
                if (!mOnTransformChildrenChangedMethodGot)
                {
                    mOnTransformChildrenChangedMethod = instance.Type.GetMethod("OnTransformChildrenChanged", 0);
                    mOnTransformChildrenChangedMethodGot = true;
                }

                if (mOnTransformChildrenChangedMethod != null)
                {
                    appdomain.Invoke(mOnTransformChildrenChangedMethod, instance, null);
                }
            }

            IMethod mOnTransformParentChangedMethod;
            bool mOnTransformParentChangedMethodGot;

            void OnTransformParentChanged()
            {
                if (!mOnTransformParentChangedMethodGot)
                {
                    mOnTransformParentChangedMethod = instance.Type.GetMethod("OnTransformParentChanged", 0);
                    mOnTransformParentChangedMethodGot = true;
                }

                if (mOnTransformParentChangedMethod != null)
                {
                    appdomain.Invoke(mOnTransformParentChangedMethod, instance, null);
                }
            }

            IMethod mOnTriggerEnterMethod;
            bool mOnTriggerEnterMethodGot;

            void OnTriggerEnter(Collider other)
            {
                if (!mOnTriggerEnterMethodGot)
                {
                    mOnTriggerEnterMethod = instance.Type.GetMethod("OnTriggerEnter", 1);
                    mOnTriggerEnterMethodGot = true;
                }

                if (mOnTriggerEnterMethod != null)
                {
                    appdomain.Invoke(mOnTriggerEnterMethod, instance, other);
                }
            }

            IMethod mOnTriggerEnter2DMethod;
            bool mOnTriggerEnter2DMethodGot;

            void OnTriggerEnter2D(Collider2D other)
            {
                if (!mOnTriggerEnter2DMethodGot)
                {
                    mOnTriggerEnter2DMethod = instance.Type.GetMethod("OnTriggerEnter2D", 1);
                    mOnTriggerEnter2DMethodGot = true;
                }

                if (mOnTriggerEnter2DMethod != null)
                {
                    appdomain.Invoke(mOnTriggerEnter2DMethod, instance, other);
                }
            }

            IMethod mOnTriggerExitMethod;
            bool mOnTriggerExitMethodGot;

            void OnTriggerExit(Collider other)
            {
                if (!mOnTriggerExitMethodGot)
                {
                    mOnTriggerExitMethod = instance.Type.GetMethod("OnTriggerExit", 1);
                    mOnTriggerExitMethodGot = true;
                }

                if (mOnTriggerExitMethod != null)
                {
                    appdomain.Invoke(mOnTriggerExitMethod, instance, other);
                }
            }

            IMethod mOnTriggerExit2DMethod;
            bool mOnTriggerExit2DMethodGot;

            void OnTriggerExit2D(Collider2D other)
            {
                if (!mOnTriggerExit2DMethodGot)
                {
                    mOnTriggerExit2DMethod = instance.Type.GetMethod("OnTriggerExit2D", 1);
                    mOnTriggerExit2DMethodGot = true;
                }

                if (mOnTriggerExit2DMethod != null)
                {
                    appdomain.Invoke(mOnTriggerExit2DMethod, instance, other);
                }
            }

            IMethod mOnTriggerStayMethod;
            bool mOnTriggerStayMethodGot;

            void OnTriggerStay(Collider other)
            {
                if (!mOnTriggerStayMethodGot)
                {
                    mOnTriggerStayMethod = instance.Type.GetMethod("OnTriggerStay", 1);
                    mOnTriggerStayMethodGot = true;
                }

                if (mOnTriggerStayMethod != null)
                {
                    appdomain.Invoke(mOnTriggerStayMethod, instance, other);
                }
            }

            IMethod mOnTriggerStay2DMethod;
            bool mOnTriggerStay2DMethodGot;

            void OnTriggerStay2D(Collider2D other)
            {
                if (!mOnTriggerStay2DMethodGot)
                {
                    mOnTriggerStay2DMethod = instance.Type.GetMethod("OnTriggerStay2D", 1);
                    mOnTriggerStay2DMethodGot = true;
                }

                if (mOnTriggerStay2DMethod != null)
                {
                    appdomain.Invoke(mOnTriggerStay2DMethod, instance, other);
                }
            }

            IMethod mOnValidateMethod;
            bool mOnValidateMethodGot;

            public void OnValidate()
            {
                if (instance != null)
                {
                    if (!mOnValidateMethodGot)
                    {
                        mOnValidateMethod = instance.Type.GetMethod("OnValidate", 0);
                        mOnValidateMethodGot = true;
                    }

                    if (mOnValidateMethod != null)
                    {
                        appdomain.Invoke(mOnValidateMethod, instance, null);
                    }
                }
            }

            IMethod mOnWillRenderObjectMethod;
            bool mOnWillRenderObjectMethodGot;

            void OnWillRenderObject()
            {
                if (!mOnWillRenderObjectMethodGot)
                {
                    mOnWillRenderObjectMethod = instance.Type.GetMethod("OnWillRenderObject", 0);
                    mOnWillRenderObjectMethodGot = true;
                }

                if (mOnWillRenderObjectMethod != null)
                {
                    appdomain.Invoke(mOnWillRenderObjectMethod, instance, null);
                }
            }


            public override string ToString()
            {
                IMethod m = appdomain.ObjectType.GetMethod("ToString", 0);
                m = instance.Type.GetVirtualMethod(m);
                if (m == null || m is ILMethod)
                {
                    return instance.ToString();
                }
                else
                    return instance.Type.FullName;
            }
        }
    }
}