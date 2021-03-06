/* Exit Games Photon LoadBalancing - C++ Client Lib
 * Copyright (C) 2004-2014 by Exit Games GmbH. All rights reserved.
 * http://www.exitgames.com
 * mailto:developer@exitgames.com
 */

#include "LoadBalancing-cpp/inc/Client.h"
#include "LoadBalancing-cpp/inc/Enums/CustomAuthenticationType.h"
#include "LoadBalancing-cpp/inc/Enums/DisconnectCause.h"
#include "LoadBalancing-cpp/inc/Internal/AuthenticationValuesSecretSetter.h"
#include "LoadBalancing-cpp/inc/Internal/PlayerMovementInformant.h"
#include "LoadBalancing-cpp/inc/Internal/Utils.h"
#include "LoadBalancing-cpp/inc/Internal/Enums/EventCode.h"
#include "LoadBalancing-cpp/inc/Internal/Enums/JoinType.h"
#include "LoadBalancing-cpp/inc/Internal/Enums/OperationCode.h"
#include "LoadBalancing-cpp/inc/Internal/Enums/ParameterCode.h"
#include "LoadBalancing-cpp/inc/Internal/Enums/Properties/Player.h"
#include "LoadBalancing-cpp/inc/Internal/Enums/Properties/Room.h"

/** @file LoadBalancing-cpp/inc/Client.h */

namespace ExitGames
{
	namespace LoadBalancing
	{
		using namespace Common;
		using namespace Common::MemoryManagement;
		using namespace Photon;
		using namespace Photon::StatusCode;
		using namespace Internal;

		const JString Client::NAMESERVER = L"ns.exitgamescloud.com"; // default name server address
		
		const bool SEND_AUTHENTICATE_ENCRYPTED = true;

		int Client::getServerTimeOffset(void) const
		{
			return mPeer.getServerTimeOffset();
		}

		int Client::getServerTime(void) const
		{
			return mPeer.getServerTime();
		}

		int Client::getBytesOut(void) const
		{
			return mPeer.getBytesOut();
		}

		int Client::getBytesIn(void) const
		{
			return mPeer.getBytesIn();
		}

		int Client::getByteCountCurrentDispatch(void) const
		{
			return mPeer.getByteCountCurrentDispatch();
		}

		int Client::getByteCountLastOperation(void) const
		{
			return mPeer.getByteCountLastOperation();
		}

		int Client::getSentCountAllowance(void) const
		{
			return mPeer.getSentCountAllowance();
		}

		void Client::setSentCountAllowance(int setSentCountAllowance)
		{
			mPeer.setSentCountAllowance(setSentCountAllowance);
		}

		int Client::getTimePingInterval(void) const
		{
			return mPeer.getTimePingInterval();
		}

		void Client::setTimePingInterval(int setTimePingInterval)
		{
			mPeer.setTimePingInterval(setTimePingInterval);
		}

		int Client::getRoundTripTime(void) const
		{
			return mPeer.getRoundTripTime();
		}

		int Client::getRoundTripTimeVariance(void) const
		{
			return mPeer.getRoundTripTimeVariance();
		}

		int Client::getTimestampOfLastSocketReceive(void) const
		{
			return mPeer.getTimestampOfLastSocketReceive();
		}

		Common::DebugLevel::DebugLevel Client::getDebugOutputLevel(void) const
		{
			return mPeer.getDebugOutputLevel();
		}

		bool Client::setDebugOutputLevel(Common::DebugLevel::DebugLevel debugLevel)
		{
			return mLogger.setDebugOutputLevel(debugLevel) && mPeer.setDebugOutputLevel(debugLevel);
		}

		int Client::getIncomingReliableCommandsCount(void) const
		{
			return mPeer.getIncomingReliableCommandsCount();
		}

		short Client::getPeerId(void) const
		{
			return mPeer.getPeerId();
		}

		int Client::getDisconnectTimeout(void) const
		{
			return mPeer.getDisconnectTimeout();
		}

		void Client::setDisconnectTimeout(int disconnectTimeout)
		{
			mPeer.setDisconnectTimeout(disconnectTimeout);
		}

		int Client::getQueuedIncomingCommands(void) const
		{
			return mPeer.getQueuedIncomingCommands();
		}

		int Client::getQueuedOutgoingCommands(void) const
		{
			return mPeer.getQueuedOutgoingCommands();
		}

		bool Client::getIsEncryptionAvailable(void) const
		{
			return mPeer.getIsEncryptionAvailable();
		}

		int Client::getResentReliableCommands(void) const
		{
			return mPeer.getResentReliableCommands();
		}

		int Client::getLimitOfUnreliableCommands(void) const
		{
			return mPeer.getLimitOfUnreliableCommands();
		}

		void Client::setLimitOfUnreliableCommands(int value)
		{
			mPeer.setLimitOfUnreliableCommands(value);
		}

		void Client::setCrcEnabled(bool crcEnabled)
		{
			mPeer.setCrcEnabled(crcEnabled);
		}

		bool Client::getCrcEnabled(void) const
		{
			return mPeer.getCrcEnabled();
		}

		int Client::getPacketLossByCrc(void) const
		{
			return mPeer.getPacketLossByCrc();
		}

		void Client::setTrafficStatsEnabled(bool trafficStatsEnabled)
		{
			mPeer.setTrafficStatsEnabled(trafficStatsEnabled);
		}

		bool Client::getTrafficStatsEnabled(void) const
		{
			return mPeer.getTrafficStatsEnabled();
		}

		int Client::getTrafficStatsElapsedMs(void) const
		{
			return mPeer.getTrafficStatsElapsedMs();
		}

		const Photon::TrafficStats& Client::getTrafficStatsIncoming(void) const
		{
			return mPeer.getTrafficStatsIncoming();
		}

		const Photon::TrafficStats& Client::getTrafficStatsOutgoing(void) const
		{
			return mPeer.getTrafficStatsOutgoing();
		}

		const Photon::TrafficStatsGameLevel& Client::getTrafficStatsGameLevel(void) const
		{
			return mPeer.getTrafficStatsGameLevel();
		}

		short Client::getPeerCount(void)
		{
			return Peer::getPeerCount();
		}

		PeerStates::PeerStates Client::getState(void) const
		{
			return mState;
		}

		const JString& Client::getMasterserverAddress(void) const
		{
			return mMasterserver;
		}

		int Client::getCountPlayersIngame(void) const
		{
			return mPeerCount;
		}

		int Client::getCountGamesRunning(void) const
		{
			return mRoomCount;
		}

		int Client::getCountPlayersOnline(void) const
		{
			return mPeerCount + mMasterPeerCount;
		}

		MutableRoom& Client::getCurrentlyJoinedRoom(void)
		{
			if(!mpCurrentlyJoinedRoom)
				mpCurrentlyJoinedRoom = createMutableRoom(L"", Hashtable(), Common::JVector<Common::JString>(), 0, 0); 
			return *mpCurrentlyJoinedRoom;
		}

		const JVector<Room*>& Client::getRoomList(void) const
		{
			return mRoomList;
		}
		
		const JVector<JString>& Client::getRoomNameList(void) const
		{
			return mRoomNameList;
		}

		bool Client::getIsInRoom(void) const
		{
			return getIsInGameRoom() || getIsInLobby();
		}
		
		bool Client::getIsInGameRoom(void) const
		{
			return mState == PeerStates::Joined;
		}

		bool Client::getIsInLobby(void) const
		{
			return mState == PeerStates::JoinedLobby;
		}

		bool Client::getAutoJoinLobby(void) const
		{
			return mAutoJoinLobby;
		}

		void Client::setAutoJoinLobby(bool onConnect)
		{
			mAutoJoinLobby = onConnect;
		}

		MutablePlayer& Client::getLocalPlayer(void)
		{
			if(!mpLocalPlayer)
				mpLocalPlayer = createMutablePlayer(-1, Hashtable()); 
			return *mpLocalPlayer;
		}

		const JVector<FriendInfo>& Client::getFriendList(void)
		{
			return mFriendList;
		}

		int Client::getFriendListAge(void)
		{
			return mIsFetchingFriendList||!mFriendListTimestamp ? 0 : GETTIMEMS()-mFriendListTimestamp;
		}

		/**
			Summarizes (aggregates) the different causes for disconnects of a client.
			A disconnect can be caused by: errors in the network connection or some vital operation failing
			(which is considered "high level"). While operations always trigger a call to OnOperationResponse,
			connection related changes are treated in OnStatusChanged.
			The DisconnectCause is set in either case and summarizes the causes for any disconnect in a single
			state value which can be used to display (or debug) the cause for disconnection.
		*/
		int Client::getDisconnectedCause(void)
		{
			return mDisconnectedCause;
		}

		Client::Client(Listener& listener, const JString& applicationID, const JString& appVersion, const JString& username, nByte connectionProtocol, AuthenticationValues authenticationValues, bool autoLobbyStats, bool useDefaultRegion)

#if defined _EG_MS_COMPILER
#	pragma warning(push)
#	pragma warning(disable:4355)
#endif
			: mPeer(*this, connectionProtocol)
			, mListener(listener)
			, mAppVersion(appVersion)
			, mAppID(applicationID)
			, mPeerCount(0)
			, mRoomCount(0)
			, mMasterPeerCount(0)
			, mLastJoinType(0)
			, mLastLobbyJoinType(0)
			, mLastJoinCreateIfNotExists(false)
			, mLastJoinPlayerNumber(0)
			, mLastCacheSliceIndex(0)
			, mpCurrentlyJoinedRoom(NULL)
			, mCachedErrorCodeFromGameServer(ErrorCode::OK)
			, mAutoJoinLobby(true)
			, mpLocalPlayer(NULL)
			, mIsFetchingFriendList(false)
			, mState(PeerStates::Uninitialized)
			, mAuthenticationValues(authenticationValues)
			, mAutoLobbyStats(autoLobbyStats)
			, mDisconnectedCause(DisconnectCause::NONE)
			, M_USE_DEFAULT_REGION(useDefaultRegion)
			, M_CONNECTION_PROTOCOL(connectionProtocol)
#ifdef _EG_MS_COMPILER
#	pragma warning(pop)
#endif
		{
			mLogger.setListener(*this);
			getLocalPlayer().setName(username);
		}

		Client::~Client(void)
		{
			destroyMutableRoom(mpCurrentlyJoinedRoom);
			destroyMutablePlayer(mpLocalPlayer);
			for(unsigned int i=0; i<mRoomList.getSize(); ++i)
				destroyRoom(mRoomList[i]);
		}

		bool Client::connect(const JString& serverAddress, nByte serverType)
		{
			AuthenticationValuesSecretSetter::setSecret(mAuthenticationValues, L""); //reset secret
			mState = serverType==ServerType::NAME_SERVER?PeerStates::ConnectingToNameserver:PeerStates::Connecting;
			static const int NAMESERVER_PORT_GAP = 3;
			return mPeer.connect(serverAddress + (serverAddress.indexOf(L':') == -1?JString(L":")+((M_CONNECTION_PROTOCOL==ConnectionProtocol::UDP?NetworkPort::UDP:NetworkPort::TCP)+(serverType==ServerType::NAME_SERVER?NAMESERVER_PORT_GAP:0)):JString()));
		}

		void Client::disconnect(void)
		{
			mState = PeerStates::Disconnecting;
			mPeer.disconnect();
		}

		void Client::service(bool dispatchIncomingCommands)
		{
			mPeer.service(dispatchIncomingCommands);
		}

		void Client::serviceBasic(void)
		{
			mPeer.serviceBasic();
		}

		bool Client::opCustom(const Photon::OperationRequest& operationRequest, bool sendReliable, nByte channelID, bool encrypt)
		{
			return mPeer.opCustom(operationRequest, sendReliable, channelID, encrypt);
		}

		bool Client::sendOutgoingCommands(void)
		{
			return mPeer.sendOutgoingCommands();
		}

		bool Client::sendAcksOnly(void)
		{
			return mPeer.sendAcksOnly();
		}

		bool Client::dispatchIncomingCommands(void)
		{
			return mPeer.dispatchIncomingCommands();
		}

		bool Client::establishEncryption(void)
		{
			return mPeer.establishEncryption();
		}

		void Client::fetchServerTimestamp(void)
		{
			mPeer.fetchServerTimestamp();
		}

		void Client::resetTrafficStats(void)
		{
			mPeer.resetTrafficStats();
		}

		void Client::resetTrafficStatsMaximumCounters(void)
		{
			mPeer.resetTrafficStatsMaximumCounters();
		}

		Common::JString Client::vitalStatsToString(bool all) const
		{
			return mPeer.vitalStatsToString(all);
		}

		bool Client::opJoinLobby(const JString& lobbyName, nByte lobbyType)
		{
			if(getIsInRoom())
			{
				EGLOG(DebugLevel::ERRORS, L"already in a room");
				return false;
			}

			mLastLobbyJoinType = JoinType::EXPLICIT_JOIN_LOBBY;

			return mPeer.opJoinLobby(lobbyName, lobbyType);
		}

		bool Client::opLeaveLobby(void)
		{
			if(!getIsInLobby())
			{
				EGLOG(DebugLevel::ERRORS, L"lobby isn't currently joined");
				return false;
			}

			return opCustom(OperationRequest(OperationCode::LEAVE_LOBBY), true);
		}

		bool Client::opCreateRoom(const JString& gameID, bool isVisible, bool isOpen, nByte maxPlayers, const Hashtable& customRoomProperties, const JVector<JString>& propsListedInLobby, const Common::JString& lobbyName, nByte lobbyType, int playerTtl, int emptyRoomTtl)
		{
			if(getIsInGameRoom())
			{
				EGLOG(DebugLevel::ERRORS, L"already in a gameroom");
				return false;
			}

			mRoomName = gameID;

			OperationRequestParameters op(mPeer.opCreateRoomImplementation(gameID, isVisible, isOpen, maxPlayers, getIsOnGameServer()?customRoomProperties:Hashtable(), getIsOnGameServer()?getLocalPlayer().getCustomProperties():Hashtable(), getIsOnGameServer()?propsListedInLobby:JVector<JString>(), lobbyName, lobbyType, playerTtl, emptyRoomTtl));
			if(getLocalPlayer().getName().length())
			{
				if((ValueObject<Hashtable>*)op.getValue(static_cast<nByte>(ParameterCode::PLAYER_PROPERTIES)))
					((ValueObject<Hashtable>*)op.getValue(static_cast<nByte>(ParameterCode::PLAYER_PROPERTIES)))->getDataAddress()->put(static_cast<nByte>(Properties::Player::PLAYERNAME), getLocalPlayer().getName());
				else
				{
					Hashtable playerProp;
					playerProp.put(static_cast<nByte>(Properties::Player::PLAYERNAME), getLocalPlayer().getName());
					op.put(static_cast<nByte>(ParameterCode::PLAYER_PROPERTIES), ValueObject<Hashtable>(playerProp));
				}
			}

			if(!opCustom(OperationRequest(OperationCode::CREATE_ROOM, op), true))
				return false;

			Hashtable roomProps(Utils::stripToCustomProperties(customRoomProperties));
			roomProps.put(static_cast<nByte>(Properties::Room::IS_OPEN), isOpen);
			roomProps.put(static_cast<nByte>(Properties::Room::IS_VISIBLE), isVisible);
			roomProps.put(static_cast<nByte>(Properties::Room::MAX_PLAYERS), maxPlayers);
			JString* propsListedInLobbyArr = allocateArray<JString>(propsListedInLobby.getSize());
			for(unsigned int i=0; i<propsListedInLobby.getSize(); ++i)
				propsListedInLobbyArr[i] = propsListedInLobby[i];
			roomProps.put(static_cast<nByte>(Properties::Room::PROPS_LISTED_IN_LOBBY), propsListedInLobbyArr, propsListedInLobby.getSize());
			deallocateArray(propsListedInLobbyArr);

			MutableRoom* oldRoom = mpCurrentlyJoinedRoom;
			mpCurrentlyJoinedRoom = createMutableRoom(gameID, roomProps,  propsListedInLobby, playerTtl, emptyRoomTtl);
			destroyMutableRoom(oldRoom);
			return true;
		}

		bool Client::opJoinRoom(const JString& gameID, bool createIfNotExists, int playerNumber, int cacheSliceIndex, int playerTtl, int emptyRoomTtl)
		{
			if(getIsInGameRoom())
			{
				EGLOG(DebugLevel::ERRORS, L"already in a gameroom");
				return false;
			}

			// TODO:
			if(playerNumber) // (workaround for some server issue we will fix)
				createIfNotExists = true;

			mRoomName = gameID;
			OperationRequestParameters op = mPeer.opJoinRoomImplementation(gameID, getIsOnGameServer()?getLocalPlayer().getCustomProperties():Hashtable(), createIfNotExists, playerNumber, getIsOnGameServer()?cacheSliceIndex:0, playerTtl, emptyRoomTtl);
			if(getLocalPlayer().getName().length())
			{
				if((ValueObject<Hashtable>*)op.getValue(static_cast<nByte>(ParameterCode::PLAYER_PROPERTIES)))
					((ValueObject<Hashtable>*)op.getValue(static_cast<nByte>(ParameterCode::PLAYER_PROPERTIES)))->getDataAddress()->put(static_cast<nByte>(Properties::Player::PLAYERNAME), getLocalPlayer().getName());
				else
				{
					Hashtable playerProp;
					playerProp.put(static_cast<nByte>(Properties::Player::PLAYERNAME), getLocalPlayer().getName());
					op.put(static_cast<nByte>(ParameterCode::PLAYER_PROPERTIES), ValueObject<Hashtable>(playerProp));
				}
			}

			if(!gameID.length() || !opCustom(OperationRequest(OperationCode::JOIN_ROOM, op), true))
				return false;

			MutableRoom* oldRoom = mpCurrentlyJoinedRoom;
			mpCurrentlyJoinedRoom = createMutableRoom(gameID, Hashtable(), JVector<JString>(), playerTtl, emptyRoomTtl);
			destroyMutableRoom(oldRoom);
			mLastJoinCreateIfNotExists = createIfNotExists;
			mLastJoinPlayerNumber = playerNumber;
			mLastCacheSliceIndex = cacheSliceIndex;
			return true;
		}

		bool Client::opJoinRandomRoom(const Hashtable& customRoomProperties, nByte maxPlayers, nByte matchmakingMode, const JString& lobbyName, nByte lobbyType, const JString& sqlLobbyFilter)
		{
			if(getIsInGameRoom())
			{
				EGLOG(DebugLevel::ERRORS, L"already in a gameroom");
				return false;
			}
			
			if(!mPeer.opJoinRandomRoom(customRoomProperties, maxPlayers, matchmakingMode, lobbyName, lobbyType, sqlLobbyFilter))
				return false;

			MutableRoom* oldRoom = mpCurrentlyJoinedRoom;
			mpCurrentlyJoinedRoom = createMutableRoom(L"", Utils::stripToCustomProperties(customRoomProperties), Common::JVector<Common::JString>(), 0, 0);
			destroyMutableRoom(oldRoom);
			return true;
		}

		bool Client::opLeaveRoom(bool willComeBack)
		{
			if(!getIsInGameRoom())
			{
				EGLOG(DebugLevel::ERRORS, L"no gameroom is currently joined");
				return false;
			}
			if(willComeBack)
			{
				mState = PeerStates::DisconnectingFromGameserver;
				mPeer.disconnect();
			}
			else
			{
				if(!mPeer.opLeaveRoom())
					return false;
				mState = PeerStates::Leaving;
			}
			return true;
		}
		
		bool Client::opFindFriends(const JString* friendsToFind, short numFriendsToFind)
		{
			if(getIsOnGameServer() || mIsFetchingFriendList)
				return false;
			mLastFindFriendsRequest.removeAllElements();
			for(short i=0; i<numFriendsToFind; ++i)
				mLastFindFriendsRequest.addElement(friendsToFind[i]);
			return mIsFetchingFriendList = mPeer.opFindFriends(friendsToFind, numFriendsToFind);
		}

		bool Client::opLobbyStats(const Common::JVector<LoadBalancing::LobbyStats>& lobbiesToQuery)
		{
			if(!getIsInLobby())
			{
				EGLOG(DebugLevel::ERRORS, L"lobby isn't currently joined");
				return false;
			}
			mLobbyStatsList = lobbiesToQuery;
			return mPeer.opLobbyStats(lobbiesToQuery);
		}

		bool Client::opCustomAuthenticationSendNextStepData(const AuthenticationValues& authenticationValues)
		{
			if(mState != PeerStates::WaitingForCustomAuthenticationNextStepCall)
				return false;
			mState = PeerStates::ConnectedToNameserver;
			return mPeer.opAuthenticate(mAppID, mAppVersion, SEND_AUTHENTICATE_ENCRYPTED, getLocalPlayer().getName(), authenticationValues, mAutoLobbyStats, mSelectedRegion);
		}

		bool Client::selectRegion(const JString& selectedRegion)
		{
			if(M_USE_DEFAULT_REGION)
			{
				EGLOG(DebugLevel::ERRORS, L"this function should only be called, when you have explicitly specified in the constructor not to use the default region.");
				return false;
			}
			else
				return mPeer.opAuthenticate(mAppID, mAppVersion, SEND_AUTHENTICATE_ENCRYPTED, getLocalPlayer().getName(), mAuthenticationValues, mAutoLobbyStats, mSelectedRegion=selectedRegion);
		}

		// protocol implementations

		void Client::onOperationResponse(const OperationResponse& operationResponse)
		{
			EGLOG(operationResponse.getReturnCode()?DebugLevel::ERRORS:DebugLevel::INFO, operationResponse.toString(true));

			// Use the secret whenever we get it, no matter the operation code.
			if(operationResponse.getParameters().contains(ParameterCode::SECRET) && mAuthenticationValues.getType() != CustomAuthenticationType::NONE)
			{
				AuthenticationValuesSecretSetter::setSecret(mAuthenticationValues, ValueObject<JString>(operationResponse.getParameterForCode(ParameterCode::SECRET)).getDataCopy());
				EGLOG(DebugLevel::INFO, L"Server returned secret");
				mListener.onSecretReceival(mAuthenticationValues.getSecret());
			}

			switch(operationResponse.getOperationCode())
			{
			case OperationCode::AUTHENTICATE:
				{
					PeerStates::PeerStates oldState = mState;
					if(operationResponse.getReturnCode())
					{
						EGLOG(DebugLevel::ERRORS, L"authentication failed with errorcode %d: %ls", operationResponse.getReturnCode(), operationResponse.getDebugMessage().cstr());
						
						switch(operationResponse.getReturnCode())
						{
						case ErrorCode::INVALID_AUTHENTICATION:
							mDisconnectedCause = DisconnectCause::INVALID_AUTHENTICATION;
							break;
						case ErrorCode::CUSTOM_AUTHENTICATION_FAILED:
							mDisconnectedCause = DisconnectCause::CUSTOM_AUTHENTICATION_FAILED;
							break;
						case ErrorCode::INVALID_REGION:
							mDisconnectedCause = DisconnectCause::INVALID_REGION;
							break;
						case ErrorCode::MAX_CCU_REACHED:
							mDisconnectedCause = DisconnectCause::MAX_CCU_REACHED;
							break;
						case ErrorCode::OPERATION_DENIED:
							mDisconnectedCause = DisconnectCause::OPERATION_NOT_ALLOWED_IN_CURRENT_STATE;
							break;
						}

						handleConnectionFlowError(oldState, operationResponse.getReturnCode(), operationResponse.getDebugMessage());
						break;
					}
					else
					{
						if(mState == PeerStates::ConnectedToNameserver)
						{
							if(operationResponse.getParameters().contains(ParameterCode::DATA))
							{
								mState = PeerStates::WaitingForCustomAuthenticationNextStepCall;
								mListener.onCustomAuthenticationIntermediateStep(*ValueObject<Dictionary<JString, Object> >(operationResponse.getParameterForCode(ParameterCode::DATA)).getDataAddress());
								break;
							}
							else
							{
								mState = PeerStates::DisconnectingFromNameserver;
								mMasterserver = ValueObject<JString>(operationResponse.getParameterForCode(ParameterCode::ADDRESS)).getDataCopy();
								mPeer.disconnect();
							}
						}
						else if(mState == PeerStates::Connected || mState == PeerStates::ConnectedComingFromGameserver)
						{
							mState = mState==PeerStates::Connected?PeerStates::Authenticated:PeerStates::AuthenticatedComingFromGameserver;
							if(mAutoJoinLobby)
							{
								opJoinLobby();
								mLastLobbyJoinType = JoinType::AUTO_JOIN_LOBBY;
							}
							else
								onConnectToMasterFinished(oldState==PeerStates::ConnectedComingFromGameserver);
						}
						else if(mState == PeerStates::ConnectedToGameserver)
						{
							mState = PeerStates::Joining;
							if(mLastJoinType == JoinType::CREATE_ROOM)
								opCreateRoom(mRoomName, mpCurrentlyJoinedRoom->getIsVisible(), mpCurrentlyJoinedRoom->getIsOpen(), mpCurrentlyJoinedRoom->getMaxPlayers(), mpCurrentlyJoinedRoom->getCustomProperties(), mpCurrentlyJoinedRoom->getPropsListedInLobby(), 
									Common::JString(), LobbyType::DEFAULT, mpCurrentlyJoinedRoom->getPlayerTtl(), mpCurrentlyJoinedRoom->getRoomTtl()); // lobbyName and lobbyType not used on game server
							else if(mLastJoinType == JoinType::JOIN_ROOM)
								opJoinRoom(mRoomName, mLastJoinCreateIfNotExists, mLastJoinPlayerNumber, mLastCacheSliceIndex, mpCurrentlyJoinedRoom->getPlayerTtl(), mpCurrentlyJoinedRoom->getRoomTtl());
							else if(mLastJoinType == JoinType::JOIN_RANDOM_ROOM)
								opJoinRoom(mRoomName);
						}
					}
				}
				break;
			case OperationCode::CREATE_ROOM:
			case OperationCode::JOIN_ROOM:
				{
					if(getIsOnGameServer())
					{
						if(operationResponse.getReturnCode())
						{
							EGLOG(DebugLevel::ERRORS, L"%ls failed with errorcode %d: %ls. Client is therefore returning to masterserver!", operationResponse.getOperationCode()==OperationCode::CREATE_ROOM?L"opCreateRoom":L"opJoinRoom", operationResponse.getReturnCode(), operationResponse.getDebugMessage().cstr());
							handleConnectionFlowError(mState, operationResponse.getReturnCode(), operationResponse.getDebugMessage());
							break;
						}

						mState = PeerStates::Joined;
						int nr = ValueObject<int>(operationResponse.getParameterForCode(ParameterCode::PLAYERNR)).getDataCopy();
						Hashtable properties = getLocalPlayer().getCustomProperties();
						properties.put(static_cast<nByte>(Properties::Player::PLAYERNAME), getLocalPlayer().getName());
						destroyMutablePlayer(mpLocalPlayer);
						PlayerMovementInformant::onEnterLocal(*mpCurrentlyJoinedRoom, *(mpLocalPlayer=createMutablePlayer(nr, properties)));

						Hashtable roomProperties(ValueObject<Hashtable>(operationResponse.getParameterForCode(ParameterCode::ROOM_PROPERTIES)).getDataCopy());
						Hashtable playerProperties(ValueObject<Hashtable>(operationResponse.getParameterForCode(ParameterCode::PLAYER_PROPERTIES)).getDataCopy());
						const JVector<Object>& numbers = playerProperties.getKeys();
						for(unsigned int i=0 ; i<numbers.getSize() ; ++i)
							PlayerMovementInformant::onEnterRemote(*mpCurrentlyJoinedRoom, KeyObject<int>(numbers[i]).getDataCopy(), ValueObject<Hashtable>(playerProperties.getValue(numbers[i])).getDataCopy());
						readoutProperties(roomProperties, playerProperties, true, 0);

						switch(mLastJoinType)
						{
						case JoinType::CREATE_ROOM:
							mListener.createRoomReturn(nr, roomProperties, playerProperties, operationResponse.getReturnCode(), operationResponse.getDebugMessage());
							break;
						case JoinType::JOIN_ROOM:
							mListener.joinRoomReturn(nr, roomProperties, playerProperties, operationResponse.getReturnCode(), operationResponse.getDebugMessage());
							break;
						case JoinType::JOIN_RANDOM_ROOM:
							mListener.joinRandomRoomReturn(nr, roomProperties, playerProperties, operationResponse.getReturnCode(), operationResponse.getDebugMessage());
							break;
						default:
							break;
						}
						break;
					}
					else
					{
						switch(operationResponse.getOperationCode())
						{
						case OperationCode::CREATE_ROOM:
							{
								if(operationResponse.getReturnCode())
								{
									EGLOG(DebugLevel::ERRORS, L"opCreateRoom failed with errorcode %d: %ls. Client is therefore staying on masterserver!", operationResponse.getReturnCode(), operationResponse.getDebugMessage().cstr());
									mListener.createRoomReturn(0, Hashtable(), Hashtable(), operationResponse.getReturnCode(), operationResponse.getDebugMessage());
									break;
								}
								JString gameID = ValueObject<JString>(operationResponse.getParameterForCode(ParameterCode::ROOM_NAME)).getDataCopy();
								if(gameID.length()) // is only sent by the server's response, if it has not been sent with the client's request before!
									mRoomName = gameID;

								mGameserver = ValueObject<JString>(operationResponse.getParameterForCode(ParameterCode::ADDRESS)).getDataCopy();
								mState = PeerStates::DisconnectingFromMasterserver;
								mPeer.disconnect();
								mLastJoinType = JoinType::CREATE_ROOM;
							}
							break;
						case OperationCode::JOIN_ROOM:
							if(operationResponse.getReturnCode())
							{
								EGLOG(DebugLevel::ERRORS, L"opJoinRoom failed with errorcode %d: %ls. Client is therefore staying on masterserver!", operationResponse.getReturnCode(), operationResponse.getDebugMessage().cstr());
								mListener.joinRoomReturn(0, Hashtable(), Hashtable(), operationResponse.getReturnCode(), operationResponse.getDebugMessage());
								break;
							}

							mGameserver = ValueObject<JString>(operationResponse.getParameterForCode(ParameterCode::ADDRESS)).getDataCopy();
							mState = PeerStates::DisconnectingFromMasterserver;
							mPeer.disconnect();
							mLastJoinType = JoinType::JOIN_ROOM;
							break;
						default:
							break;
						}
					}
				}
				break;
			case OperationCode::JOIN_RANDOM_ROOM:
				if(operationResponse.getReturnCode())
				{
					EGLOG(DebugLevel::ERRORS, L"opJoinRandomRoom failed with errorcode %d: %ls. Client is therefore staying on masterserver!", operationResponse.getReturnCode(), operationResponse.getDebugMessage().cstr());
					mListener.joinRandomRoomReturn(0, Hashtable(), Hashtable(), operationResponse.getReturnCode(), operationResponse.getDebugMessage());
					break;
				}

				// store the ID of the random game, joined on the masterserver, so that we know, which game to join on the gameserver
				mRoomName = ValueObject<JString>(operationResponse.getParameterForCode(ParameterCode::ROOM_NAME)).getDataCopy();
				mGameserver = ValueObject<JString>(operationResponse.getParameterForCode(ParameterCode::ADDRESS)).getDataCopy();
				mState = PeerStates::DisconnectingFromMasterserver;
				mPeer.disconnect();
				mLastJoinType = JoinType::JOIN_RANDOM_ROOM;
				break;
			case OperationCode::JOIN_LOBBY:
				mState = PeerStates::JoinedLobby;
				if(mLastLobbyJoinType == JoinType::AUTO_JOIN_LOBBY)
					onConnectToMasterFinished(mState==PeerStates::AuthenticatedComingFromGameserver);
				else
					mListener.joinLobbyReturn();
				break;
			case OperationCode::LEAVE_LOBBY:
				{
					mState = PeerStates::Authenticated;
					mListener.leaveLobbyReturn();
				}
				break;
			case OperationCode::LEAVE:
				{
					PlayerMovementInformant::onLeaveLocal(*mpCurrentlyJoinedRoom, getLocalPlayer().getNumber());
					Hashtable op = getLocalPlayer().getCustomProperties();
					op.put(static_cast<nByte>(Properties::Player::PLAYERNAME), getLocalPlayer().getName());
					destroyMutablePlayer(mpLocalPlayer);
					mpLocalPlayer = createMutablePlayer(-1, op);
					mIsFetchingFriendList = false;
					mState = PeerStates::DisconnectingFromGameserver;
					mPeer.disconnect();
				}
				break;
			case OperationCode::FIND_FRIENDS:
				{
					mIsFetchingFriendList = false;
					if(operationResponse.getReturnCode())
					{
						EGLOG(DebugLevel::ERRORS, L"opFindFriends failed with errorcode %d: %ls.", operationResponse.getReturnCode(), operationResponse.getDebugMessage().cstr());
						break;
					}
					ValueObject<bool*> onlineList = operationResponse.getParameterForCode(ParameterCode::FIND_FRIENDS_RESPONSE_ONLINE_LIST);
					ValueObject<JString*> roomList = operationResponse.getParameterForCode(ParameterCode::FIND_FRIENDS_RESPONSE_ROOM_ID_LIST);
					bool* pOnlineList = *onlineList.getDataAddress();
					JString* pRoomList = *roomList.getDataAddress();
					mFriendList.removeAllElements();
					for(unsigned int i=0; i<mLastFindFriendsRequest.getSize(); ++i) {
						if((int)i < onlineList.getSizes()[0] && (int)i < roomList.getSizes()[0]) {
							mFriendList.addElement(FriendInfo(mLastFindFriendsRequest[i], pOnlineList[i], pRoomList[i]));
						}
					}
					if(!(mFriendListTimestamp=GETTIMEMS()))
						mFriendListTimestamp = 1;
					mListener.onFindFriendsResponse();
				}
				break;
			case OperationCode::LOBBY_STATS:
				{
					if(operationResponse.getReturnCode())
					{
						EGLOG(DebugLevel::ERRORS, L"opLobbyStats failed with errorcode %d: %ls.", operationResponse.getReturnCode(), operationResponse.getDebugMessage().cstr());
						break;
					}
					ValueObject<JString*> namesList = operationResponse.getParameterForCode(ParameterCode::LOBBY_NAME);
					ValueObject<nByte*> typesList = operationResponse.getParameterForCode(ParameterCode::LOBBY_TYPE);
					ValueObject<int*> peersList = operationResponse.getParameterForCode(ParameterCode::PEER_COUNT);
					ValueObject<int*> roomsList = operationResponse.getParameterForCode(ParameterCode::ROOM_COUNT);

					int* peers = *peersList.getDataAddress();
					int* rooms = *roomsList.getDataAddress();

					JVector<LobbyStats> res;
					if(namesList.getType() != TypeCode::EG_NULL) 
					{
						JString* names = *namesList.getDataAddress();
						nByte* types = *typesList.getDataAddress();
						for(int i=0; i<*namesList.getSizes(); ++i) 
							res.addElement(LobbyStats(names[i], types[i], peers[i], rooms[i]));
					}
					else 
					{
						for(int i=0; i<static_cast<int>(mLobbyStatsList.getSize()); ++i) 
						{
							int peerCount = i < *peersList.getSizes() ? peers[i] : 0;
							int roomCount = i < *roomsList.getSizes() ? rooms[i] : 0;
							res.addElement(LobbyStats(mLobbyStatsList[i].getName(), mLobbyStatsList[i].getType(), peerCount, roomCount));
						}
					}
					mListener.onLobbyStatsResponse(res);
				}
				break;
			case OperationCode::GET_REGIONS:
                {
                    if(operationResponse.getReturnCode())
                    {
                        EGLOG(DebugLevel::ERRORS, L"GetRegions failed with errorcode %d: %ls.", operationResponse.getReturnCode(), operationResponse.getDebugMessage().cstr());
                        break;
                    }
                    JVector<JString> availableRegions(*ValueObject<JString*>(operationResponse.getParameterForCode(ParameterCode::REGION)).getDataAddress(), *ValueObject<JString*>(operationResponse.getParameterForCode(ParameterCode::REGION)).getSizes());
                    JVector<JString> availableRegionServers(*ValueObject<JString*>(operationResponse.getParameterForCode(ParameterCode::ADDRESS)).getDataAddress(), *ValueObject<JString*>(operationResponse.getParameterForCode(ParameterCode::ADDRESS)).getSizes());
                    if(M_USE_DEFAULT_REGION)
                        mPeer.opAuthenticate(mAppID, mAppVersion, SEND_AUTHENTICATE_ENCRYPTED, getLocalPlayer().getName(), mAuthenticationValues, mAutoLobbyStats, mSelectedRegion=availableRegions[0]);
                    else
                        mListener.onAvailableRegions(availableRegions, availableRegionServers);
                }
				break;
			case OperationCode::RPC:
				{
					if(operationResponse.getReturnCode())
						EGLOG(DebugLevel::ERRORS, L"WebRpc failed with errorcode %d: %ls.", operationResponse.getReturnCode(), operationResponse.getDebugMessage().cstr());
					int returnCode = ValueObject<int>(operationResponse.getParameterForCode(ParameterCode::RPC_CALL_RET_CODE)).getDataCopy();
					ValueObject<JString> uriPathObj = ValueObject<JString>(operationResponse.getParameterForCode(ParameterCode::URI_PATH));
					Common::JString* uriPath = uriPathObj.getDataAddress();
					ValueObject<Dictionary<Object, Object> > returnDataObj = ValueObject<Dictionary<Object, Object> >(operationResponse.getParameterForCode(ParameterCode::RPC_CALL_PARAMS));
					Dictionary<Object, Object>* returnData = returnDataObj.getDataAddress();
					mListener.webRpcReturn(operationResponse.getReturnCode(), operationResponse.getDebugMessage(), uriPath?*uriPath:JString(), returnCode, returnData?*returnData:Dictionary<Object, Object>());
				}
				break;
			default:
				break;
			}
		}

		void Client::onStatusChanged(int statusCode)
		{
			switch(statusCode)
			{
			case StatusCode::CONNECT:
				{
					if(mState == PeerStates::ConnectingToNameserver)
					{
						EGLOG(DebugLevel::INFO, L"connected to nameserver");
						mState = PeerStates::ConnectedToNameserver;
					}
					else if(mState == PeerStates::ConnectingToGameserver)
					{
						EGLOG(DebugLevel::INFO, L"connected to gameserver");
						mState = PeerStates::ConnectedToGameserver;
					}
					else
					{
						EGLOG(DebugLevel::INFO, L"connected to masterserver");
						mState = mState == PeerStates::Connecting ? PeerStates::Connected : PeerStates::ConnectedComingFromGameserver;
					}

					if(SEND_AUTHENTICATE_ENCRYPTED)
						mPeer.establishEncryption();
                    else if(mState == PeerStates::ConnectedToNameserver)
                        mPeer.opGetRegions(false, mAppID);
                    else
                        mPeer.opAuthenticate(mAppID, mAppVersion, false, getLocalPlayer().getName(), mAuthenticationValues, mAutoLobbyStats); // secret is encrypted anyway
                    break;
				}
				break;
			case StatusCode::DISCONNECT:
				{
					mIsFetchingFriendList = false;
					if(mState == PeerStates::DisconnectingFromNameserver)
					{
						mPeer.connect(mMasterserver);
						mState = PeerStates::Connecting;
					}
					else if(mState == PeerStates::DisconnectingFromMasterserver)
					{
						mPeer.connect(mGameserver);
						mState = PeerStates::ConnectingToGameserver;
					}
					else if(mState == PeerStates::DisconnectingFromGameserver)
					{
						mPeer.connect(mMasterserver);
						mState = PeerStates::ConnectingToMasterserver;
					}
					else
					{
						mState = PeerStates::PeerCreated;
						mListener.disconnectReturn();
					}
				}
				break;
			case StatusCode::ENCRYPTION_ESTABLISHED:
				if(mState == PeerStates::ConnectedToNameserver)
					mPeer.opGetRegions(true, mAppID);
				else
					mPeer.opAuthenticate(mAppID, mAppVersion, true, getLocalPlayer().getName(), mAuthenticationValues, mAutoLobbyStats);
				break;
			case StatusCode::ENCRYPTION_FAILED_TO_ESTABLISH:
				handleConnectionFlowError(mState, statusCode, "Encryption failed to establish");
				break;
			// cases till next break should set mDisconnectedCause below
			case StatusCode::EXCEPTION:
			case StatusCode::EXCEPTION_ON_CONNECT:
			case StatusCode::INTERNAL_RECEIVE_EXCEPTION:
			case StatusCode::TIMEOUT_DISCONNECT:
			case StatusCode::DISCONNECT_BY_SERVER:
			case StatusCode::DISCONNECT_BY_SERVER_USER_LIMIT:
			case StatusCode::DISCONNECT_BY_SERVER_LOGIC:
				mListener.connectionErrorReturn(statusCode);
				if(mPeer.getPeerState() != PeerState::DISCONNECTED && mPeer.getPeerState() != PeerState::DISCONNECTING)
					disconnect();
				break;
			case StatusCode::SEND_ERROR:
				mListener.clientErrorReturn(statusCode);
				break;
			case QUEUE_OUTGOING_RELIABLE_WARNING:
			case QUEUE_OUTGOING_UNRELIABLE_WARNING:
			case QUEUE_OUTGOING_ACKS_WARNING:
			case QUEUE_INCOMING_RELIABLE_WARNING:
			case QUEUE_INCOMING_UNRELIABLE_WARNING:
			case QUEUE_SENT_WARNING:
				mListener.warningReturn(statusCode);
				break;
			case ErrorCode::OPERATION_INVALID:
			case ErrorCode::INTERNAL_SERVER_ERROR:
				mListener.serverErrorReturn(statusCode);
				break;
			default:
				EGLOG(DebugLevel::ERRORS, L"received unknown status-code from server");
				break;
			}

			// above cases starting from StatusCode::EXCEPTION till next break should set mDisconnectedCause here
			switch(statusCode)
			{
			case StatusCode::DISCONNECT_BY_SERVER_USER_LIMIT:
				mDisconnectedCause = DisconnectCause::DISCONNECT_BY_SERVER_USER_LIMIT;
				break;
			case StatusCode::EXCEPTION_ON_CONNECT:
				mDisconnectedCause = DisconnectCause::EXCEPTION_ON_CONNECT;
				break;
			case StatusCode::DISCONNECT_BY_SERVER:
				mDisconnectedCause = DisconnectCause::DISCONNECT_BY_SERVER;
				break;
			case StatusCode::DISCONNECT_BY_SERVER_LOGIC:
				mDisconnectedCause = DisconnectCause::DISCONNECT_BY_SERVER_LOGIC;
				break;
			case StatusCode::TIMEOUT_DISCONNECT:
				mDisconnectedCause = DisconnectCause::TIMEOUT_DISCONNECT;
				break;
			case StatusCode::EXCEPTION:
			case StatusCode::INTERNAL_RECEIVE_EXCEPTION:
				mDisconnectedCause = DisconnectCause::EXCEPTION;
				break;
			}
		}

		void Client::onEvent(const EventData& eventData)
		{
			EGLOG(DebugLevel::ALL, L"%ls", eventData.toString(true).cstr()); // don't print out the payload here, as that can get too expensive for big events
			switch(eventData.getCode())
			{
			case EventCode::ROOM_LIST:
				{
					for(unsigned int i=0; i<mRoomList.getSize(); ++i)
						destroyRoom(mRoomList[i]);
					mRoomList.removeAllElements();
					mRoomNameList.removeAllElements();

					Hashtable roomList = ValueObject<Hashtable>(eventData.getParameterForCode(ParameterCode::ROOM_LIST)).getDataCopy();
					const JVector<Object>& keys = roomList.getKeys();
					JString roomNameValue;
					for(unsigned int i=0 ; i<keys.getSize(); ++i)
					{
						roomNameValue = KeyObject<JString>(keys[i]).getDataCopy();
						mRoomList.addElement(createRoom(roomNameValue, ValueObject<Hashtable>(roomList.getValue(keys[i])).getDataCopy()));
						mRoomNameList.addElement(roomNameValue);
					}
					mListener.onRoomListUpdate();
				}
				break;
			case EventCode::ROOM_LIST_UPDATE:
				{
					Hashtable roomListUpdate(ValueObject<Hashtable>(eventData.getParameterForCode(ParameterCode::ROOM_LIST)).getDataCopy());
					const JVector<Object>& keys = roomListUpdate.getKeys();
					for(unsigned int i=0; i<keys.getSize(); i++)
					{
						Hashtable val(ValueObject<Hashtable>(roomListUpdate.getValue(keys[i])).getDataCopy());
						bool removed = ValueObject<bool>(val.getValue(static_cast<nByte>(Properties::Room::REMOVED))).getDataCopy();
						int index = mRoomNameList.getIndexOf(KeyObject<JString>(keys[i]).getDataCopy());

						if(!removed)
						{
							if(index == -1) //add room
							{
								JString roomNameValue = KeyObject<JString>(keys[i]).getDataCopy();
								mRoomList.addElement(createRoom(roomNameValue, val));
								mRoomNameList.addElement(roomNameValue);
							}
							else // update room (only entries, which have been changed, have been sent)
								RoomPropertiesCacher::cache(*mRoomList[index], val);
						}
						else if(index > -1) // remove room
						{
							destroyRoom(mRoomList[index]);
							mRoomList.removeElementAt(index);
							mRoomNameList.removeElementAt(index);
						}
					}
					mListener.onRoomListUpdate();
				}
				break;
			case EventCode::APP_STATS:
				{
					mPeerCount = ValueObject<int>(eventData.getParameterForCode(ParameterCode::PEER_COUNT)).getDataCopy();
					mRoomCount = ValueObject<int>(eventData.getParameterForCode(ParameterCode::ROOM_COUNT)).getDataCopy();
					mMasterPeerCount = ValueObject<int>(eventData.getParameterForCode(ParameterCode::MASTER_PEER_COUNT)).getDataCopy();
					mListener.onAppStatsUpdate();
				}
				break;
			case EventCode::LOBBY_STATS:
				{
					ValueObject<JString*> namesList = eventData.getParameterForCode(ParameterCode::LOBBY_NAME);
					ValueObject<nByte*> typesList = eventData.getParameterForCode(ParameterCode::LOBBY_TYPE);
					ValueObject<int*> peersList = eventData.getParameterForCode(ParameterCode::PEER_COUNT);
					ValueObject<int*> roomsList = eventData.getParameterForCode(ParameterCode::ROOM_COUNT);

					JString* names = *namesList.getDataAddress();
					nByte* types = *typesList.getDataAddress();
					int* peers = *peersList.getDataAddress();
					int* rooms = *roomsList.getDataAddress();

					JVector<LobbyStats> res;
					for(int i=0; i<*namesList.getSizes(); ++i) 
						res.addElement(LobbyStats(names[i], types[i], peers[i], rooms[i]));
					mListener.onLobbyStatsUpdate(res);
				}
				break;
			case EventCode::JOIN:
				{
					int nr = ValueObject<int>(eventData.getParameterForCode(ParameterCode::PLAYERNR)).getDataCopy();
					if(nr != getLocalPlayer().getNumber()) // the local player already got added in onOperationResponse cases OperationCode::CREATE_ROOM / OperationCode::JOIN_ROOM
						PlayerMovementInformant::onEnterRemote(getCurrentlyJoinedRoom(), nr, ValueObject<Hashtable>(eventData.getParameterForCode(ParameterCode::PLAYER_PROPERTIES)).getDataCopy());
					Object playersObj = eventData.getParameterForCode(ParameterCode::PLAYER_LIST);
					int* players = ValueObject<int*>(playersObj).getDataCopy();
					JVector<int> playerArr;
					int* playersPtr = players;
					for(int i=0 ; i<playersObj.getSizes()[0] ; ++i, playersPtr++)
						playerArr.addElement(*playersPtr);
					mListener.joinRoomEventAction(nr, playerArr, *getCurrentlyJoinedRoom().getPlayerForNumber(nr));
					deallocateArray(players);
				}
				break;
			case EventCode::LEAVE:
				{
					ValueObject<int> nr = eventData.getParameterForCode(ParameterCode::PLAYERNR);
					ValueObject<bool> isInactive = eventData.getParameterForCode(ParameterCode::IS_INACTIVE);
					if(isInactive.getDataCopy())
					{
						if(!PlayerPropertiesUpdateInformant::setIsInactive(*mpCurrentlyJoinedRoom, nr.getDataCopy(), true))
							EGLOG(DebugLevel::WARNINGS, L"EventCode::LEAVE - player %d who is leaving the room, has not been found in list of players, who are currently in the room", nr.getDataCopy());
					}
					else if(!PlayerMovementInformant::onLeaveRemote(getCurrentlyJoinedRoom(), nr.getDataCopy()))
							EGLOG(DebugLevel::WARNINGS, L"EventCode::LEAVE - player %d who is leaving the room, has not been found in list of players, who are currently in the room", nr.getDataCopy());
					mListener.leaveRoomEventAction(nr.getDataCopy(), isInactive.getDataCopy());
				}
				break;
			case EventCode::DISCONNECT:
				{
					ValueObject<int> nr = eventData.getParameterForCode(ParameterCode::PLAYERNR);
					if(!PlayerPropertiesUpdateInformant::setIsInactive(*mpCurrentlyJoinedRoom, nr.getDataCopy(), true))
						EGLOG(DebugLevel::WARNINGS, L"EventCode::DISCONNECT - player %d who is diconnecting the room, has not been found in list of players, who are currently in the room", nr.getDataCopy());
					mListener.disconnectEventAction(nr.getDataCopy());
				}
			case EventCode::PROPERTIES_CHANGED:
				{
					ValueObject<int> target = eventData.getParameterForCode(ParameterCode::TARGET_PLAYERNR);
					Hashtable playerProperties;
					Hashtable roomProperties;
					if(target.getDataCopy())
						playerProperties = ValueObject<Hashtable>(eventData.getParameterForCode(ParameterCode::PROPERTIES)).getDataCopy();
					else
						roomProperties = ValueObject<Hashtable>(eventData.getParameterForCode(ParameterCode::PROPERTIES)).getDataCopy();
					readoutProperties(roomProperties, playerProperties, false, target.getDataCopy());
					if(playerProperties.getSize())
						mListener.onPlayerPropertiesChange(target.getDataCopy(), playerProperties);
					else
						mListener.onRoomPropertiesChange(roomProperties);
				}
				break;
			case EventCode::CACHE_SLICE_CHANGED:
				mListener.onCacheSliceChanged(ValueObject<int>(eventData.getParameterForCode(ParameterCode::CACHE_SLICE_INDEX)).getDataCopy());
				break;
			default: // custom events are forwarded to the app
				{
					ValueObject<int> nr = eventData.getParameterForCode(ParameterCode::PLAYERNR);
					// all custom event data is stored at ParameterCode::DATA
					mListener.customEventAction(nr.getDataCopy(), eventData.getCode(), eventData.getParameterForCode(ParameterCode::DATA));
				}
				break;
			}
		}

		void Client::debugReturn(DebugLevel::DebugLevel debugLevel, const JString& string)
		{
			mListener.debugReturn(debugLevel, string);
		}

		bool Client::getIsOnGameServer(void) const
		{
			return mState >= PeerStates::ConnectingToGameserver && mState < PeerStates::ConnectingToMasterserver;
		}

		void Client::readoutProperties(Hashtable& roomProperties, Hashtable& playerProperties, bool multiplePlayers, int targetPlayerNr)
		{
			if(roomProperties.getSize())
			{
				RoomPropertiesCacher::cache(*mpCurrentlyJoinedRoom, roomProperties);
				roomProperties = Utils::stripKeysWithNullValues(Utils::stripToCustomProperties(roomProperties));
			}

			if(playerProperties.getSize())
			{
				for(unsigned int i=0; i<(multiplePlayers?playerProperties.getSize():1); ++i)
					PlayerPropertiesUpdateInformant::onUpdate(*mpCurrentlyJoinedRoom, multiplePlayers?ValueObject<int>(playerProperties.getKeys()[i]).getDataCopy():targetPlayerNr, multiplePlayers?ValueObject<Hashtable>(playerProperties[i]).getDataCopy():playerProperties);

				if(multiplePlayers)
					for(unsigned int i=0; i<playerProperties.getSize(); ++i)
						playerProperties[i] = ValueObject<Hashtable>(Utils::stripKeysWithNullValues(Utils::stripToCustomProperties(ValueObject<Hashtable>(playerProperties[i]).getDataCopy())));
				else
					playerProperties = Utils::stripKeysWithNullValues(Utils::stripToCustomProperties(playerProperties));
			}
		}

		void Client::handleConnectionFlowError(PeerStates::PeerStates oldState, int errorCode, const JString& errorString)
		{
			if(oldState == PeerStates::ConnectedToGameserver || oldState == PeerStates::AuthenticatedOnGameServer || oldState == PeerStates::Joining)
			{
				mCachedErrorCodeFromGameServer = errorCode;
				mCachedErrorStringFromGameServer = errorString;
				mState = PeerStates::DisconnectingFromGameserver;
				mPeer.disconnect();
				// response to app has to wait until back on master
			}
			else
			{
				mState = PeerStates::Disconnecting;
				mPeer.disconnect();
				mListener.connectReturn(errorCode, errorString);
			}
		}

		bool Client::opAuthenticate(const JString& appID, const JString& appVersion, bool encrypted, const JString& userID)
		{
			return mPeer.opAuthenticate(appID, appVersion, encrypted, userID);
		}

		bool Client::opChangeGroups(const JVector<nByte>* groupsToRemove, const JVector<nByte>* groupsToAdd)
		{
			if(!getIsInGameRoom())
				return false;
			return mPeer.opChangeGroups(groupsToRemove, groupsToAdd);
		}

		bool Client::opGetRegions(bool encrypted, const JString& appID)
		{
			return mPeer.opGetRegions(encrypted, appID);
		}

		bool Client::opWebRpc(const JString& uriPath, const Object& parameters)
		{
			return mPeer.opWebRpc(uriPath, parameters);
		}

		bool Client::opSetPropertiesOfPlayer(int playerNr, const Hashtable& properties)
		{
			if(!getIsInGameRoom())
				return false;
			return mPeer.opSetPropertiesOfPlayer(playerNr, properties);
		}

		bool Client::opSetPropertiesOfRoom(const Hashtable& properties, bool webForward)
		{
			if(!getIsInGameRoom())
				return false;
			return mPeer.opSetPropertiesOfRoom(properties, webForward);
		}

		void Client::onConnectToMasterFinished(bool comingFromGameserver)
		{
			if(comingFromGameserver)
			{
				if(mCachedErrorCodeFromGameServer)
				{
					switch(mLastJoinType)
					{
						case JoinType::CREATE_ROOM:
							mListener.createRoomReturn(0, Hashtable(), Hashtable(), mCachedErrorCodeFromGameServer, mCachedErrorStringFromGameServer);
							break;
						case JoinType::JOIN_ROOM:
							mListener.joinRoomReturn(0, Hashtable(), Hashtable(), mCachedErrorCodeFromGameServer, mCachedErrorStringFromGameServer);
							break;
						case JoinType::JOIN_RANDOM_ROOM:
							mListener.joinRandomRoomReturn(0, Hashtable(), Hashtable(), mCachedErrorCodeFromGameServer, mCachedErrorStringFromGameServer);
							break;
						default:
							EGLOG(DebugLevel::ERRORS, L"unexpected cached join type value");
							break;
					}
					mCachedErrorCodeFromGameServer = LoadBalancing::ErrorCode::OK;
					mCachedErrorStringFromGameServer = L"";
				}
				else
					mListener.leaveRoomReturn(0, L"");
			}
			else
				mListener.connectReturn(0, L"");
		}

		MutablePlayer* Client::createMutablePlayer(int number, const Hashtable& properties)
		{
			mpMutablePlayerFactory = getMutablePlayerFactory();
			return mpMutablePlayerFactory->create(number, properties, mpCurrentlyJoinedRoom, this);
		}

		void Client::destroyMutablePlayer(const MutablePlayer* pPlayer) const
		{
			if(pPlayer)
				mpMutablePlayerFactory->destroy(pPlayer);
		}

		Room* Client::createRoom(const JString& name, const Hashtable& properties)
		{
			return RoomFactory::create(name, properties);
		}

		void Client::destroyRoom(const Room* pRoom) const
		{
			RoomFactory::destroy(pRoom);
		}

		MutableRoom* Client::createMutableRoom(const JString& name, const Hashtable& properties, const JVector<JString>& propsListedInLobby, int playerTtl, int roomTtl)
		{
			mpMutableRoomFactory = getMutableRoomFactory();
			return mpMutableRoomFactory->create(name, properties, this, propsListedInLobby, playerTtl, roomTtl);
		}

		void Client::destroyMutableRoom(const MutableRoom* pRoom) const
		{
			if(pRoom)
				mpMutableRoomFactory->destroy(pRoom);
		}

		MutablePlayerFactory* Client::getMutablePlayerFactory(void) const
		{
			static MutablePlayerFactory fac;
			return &fac;
		}

		MutableRoomFactory* Client::getMutableRoomFactory(void) const
		{
			static MutableRoomFactory fac;
			return &fac;
		}
	}
}