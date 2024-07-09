import React, { useState, useRef } from 'react';
import { StyleSheet, View, Dimensions } from 'react-native';
import { Video, AVPlaybackStatus, ResizeMode } from 'expo-av';

const VideoPlayer: React.FC = () => {
  const video = useRef<Video>(null);
  const [status, setStatus] = useState<AVPlaybackStatus>({} as AVPlaybackStatus);

  return (
    <View style={styles.container}>
      <Video
        ref={video}
        style={styles.video}
        source={{
          uri: 'http://d23dyxeqlo5psv.cloudfront.net/big_buck_bunny.mp4', // Đường dẫn tới video của bạn
        }}
        useNativeControls
        resizeMode={ResizeMode.CONTAIN}
        isLooping
        onPlaybackStatusUpdate={status => setStatus(status)}
      />
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: '#ecf0f1',
  },
  video: {
    alignSelf: 'center',
    width: Dimensions.get('window').width,
    height: 200,
  },
});

export default VideoPlayer;
