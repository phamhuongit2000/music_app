import React, { useState, useEffect } from 'react';
import { StyleSheet, Image, TouchableOpacity, View, ViewStyle, Text } from 'react-native';
import { Audio } from 'expo-av';

interface AudioPlayerProps {
  url: string;
  img: string;
  style?: ViewStyle;
  name_music: string;
  name_singer: string;
}

const AudioPlayer: React.FC<AudioPlayerProps> = ({ url, img, style, name_music, name_singer}) => {
  const [sound, setSound] = useState<Audio.Sound | null>(null);
  const [isPlaying, setIsPlaying] = useState(false);

  useEffect(() => {
    return sound
      ? () => {
          console.log('Unloading Sound');
          sound.unloadAsync();
        }
      : undefined;
  }, [sound]);

  async function playSound() {
    console.log('Loading Sound');
    const { sound } = await Audio.Sound.createAsync({ uri: url });
    setSound(sound);

    console.log('Playing Sound');
    await sound.playAsync();
    setIsPlaying(true);
  }

  async function pauseSound() {
    if (sound) {
      console.log('Pausing Sound');
      await sound.pauseAsync();
      setIsPlaying(false);
    }
  }

  return (
    <View>
      <View style={[styles.container, style]}>
        <Image source={{ uri: img }} style={styles.image} />
        <View style={styles.buttonContainer}> 
          <TouchableOpacity onPress={isPlaying ? pauseSound : playSound}>
            <Image
              source={
                isPlaying
                  ? require('../assets/pause.png')
                  : require('../assets/play.png')
              }
              style={styles.button}
            />
          </TouchableOpacity>
        </View>
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    justifyContent: 'center',
    alignItems: 'center',
  },
  image: {
    ...StyleSheet.absoluteFillObject,
    width: 100,
    height: 100,
    resizeMode: 'cover',
    borderRadius: 20,
  },
  buttonContainer: {
    position: 'absolute', 
    zIndex: 1,
  },
  button: {
    width: 30,
    height: 30,
    opacity: 0.7
  },
});

export default AudioPlayer;
